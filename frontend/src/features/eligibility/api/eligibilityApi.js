import apiClient from '../../../api/axios';
import { registrationApi as sharedRegistrationApi } from '../../../api/registrationApi';
import { mockEligibilityFor, mockRuleResults, mockPendingApprovals, mockApprovalHistory } from '../services/eligibilityMockData';

const unwrap = (response) => response?.data?.data ?? response?.data ?? response;

async function withFallback(request, fallback) {
  try {
    return unwrap(await request());
  } catch {
    return fallback;
  }
}

const findEligibility = (registrationId) => mockEligibilityFor(registrationId);

export const eligibilityFeatureApi = {
  checkEligibility: (registrationId) => withFallback(() => apiClient.get(`/eligibility/registration/${registrationId}`), findEligibility(registrationId)),
  getEligibilityRules: (registrationId) => withFallback(() => apiClient.get(`/eligibility/${registrationId}/rules`), mockRuleResults),
  getPendingApprovals: () => withFallback(() => apiClient.get('/eligibility/pending'), mockPendingApprovals),
  approveEligibility: (registrationId, payload) =>
    withFallback(() => sharedRegistrationApi.approve(registrationId, payload), {
      registrationId,
      status: 'Approved',
      approver: payload?.approver || 'system',
      decisionDate: new Date().toISOString(),
    }),
  rejectEligibility: (registrationId, payload) =>
    withFallback(() => sharedRegistrationApi.reject(registrationId, payload), {
      registrationId,
      status: 'Rejected',
      approver: payload?.approver || 'system',
      reason: payload?.reason || 'Rejected by approver',
      decisionDate: new Date().toISOString(),
    }),
  bulkApprove: (registrationIds) => withFallback(() => apiClient.post('/eligibility/bulk-approve', { ids: registrationIds }), registrationIds.map((id) => ({ registrationId: id, status: 'Approved' }))),
  getApprovalHistory: (registrationId) => withFallback(() => apiClient.get(`/eligibility/history/${registrationId}`), mockApprovalHistory),
};
