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
  checkEligibility: (registrationId) => withFallback(() => Promise.reject(), findEligibility(registrationId)),
  getEligibilityRules: (registrationId) => withFallback(() => Promise.reject(), mockRuleResults),
  getPendingApprovals: () => withFallback(() => Promise.reject(), mockPendingApprovals),
  approveEligibility: (registrationId, payload) =>
    withFallback(() => Promise.reject(), {
      registrationId,
      status: 'Approved',
      approver: payload?.approver || 'system',
      decisionDate: new Date().toISOString(),
    }),
  rejectEligibility: (registrationId, payload) =>
    withFallback(() => Promise.reject(), {
      registrationId,
      status: 'Rejected',
      approver: payload?.approver || 'system',
      reason: payload?.reason || 'Rejected by approver',
      decisionDate: new Date().toISOString(),
    }),
  bulkApprove: (registrationIds) => withFallback(() => Promise.reject(), registrationIds.map((id) => ({ registrationId: id, status: 'Approved' }))),
  getApprovalHistory: (registrationId) => withFallback(() => Promise.reject(), mockApprovalHistory),
};
