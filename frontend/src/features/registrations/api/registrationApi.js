import { registrationApi as sharedRegistrationApi } from '../../../api/registrationApi';
import {
  mockApprovals,
  mockAudit,
  mockCommunications,
  mockEligibility,
  mockRegistrations,
  mockTimeline,
} from '../services/registrationMockData';

const unwrap = (response) => response.data?.data ?? response.data;

async function withFallback(request, fallback) {
  try {
    return unwrap(await request());
  } catch {
    return fallback;
  }
}

const findRegistration = (registrationId) => mockRegistrations.find((item) => item.registrationId === registrationId) || mockRegistrations[0];

export const registrationFeatureApi = {
  registerForDrive: (payload) =>
    withFallback(() => sharedRegistrationApi.create(payload), {
      registrationId: `REG-${Date.now()}`,
      ...payload,
      status: 'Pending',
    }),
  getMyRegistrations: (userId) => withFallback(() => sharedRegistrationApi.getByUser(userId || 'me'), mockRegistrations),
  getRegistrationById: (registrationId) => withFallback(() => sharedRegistrationApi.getById(registrationId), findRegistration(registrationId)),
  getRegistrationTimeline: () => withFallback(() => Promise.reject(), mockTimeline),
  getRegistrationEligibility: () => withFallback(() => Promise.reject(), mockEligibility),
  getRegistrationAudit: () => withFallback(() => Promise.reject(), mockAudit),
  getRegistrationCommunications: () => withFallback(() => Promise.reject(), mockCommunications),
  getPendingApprovals: () => withFallback(() => Promise.reject(), mockApprovals),
  approveRegistration: (registrationId, payload) => withFallback(() => sharedRegistrationApi.approve(registrationId, payload), { registrationId, status: 'Approved' }),
  rejectRegistration: (registrationId, payload) => withFallback(() => sharedRegistrationApi.reject(registrationId, payload), { registrationId, status: 'Rejected' }),
  scheduleExam: (registrationId, preferredSlot) => withFallback(() => sharedRegistrationApi.schedule(registrationId, preferredSlot), { registrationId, status: 'ExamScheduled' }),
  updateRegistrationStatus: ({ registrationId, status }) => withFallback(() => sharedRegistrationApi.updateStatus(registrationId, status), { registrationId, status }),
  lookupStatus: (query) => withFallback(() => Promise.reject(), findRegistration(query.registrationId || 'REG-2026-0001')),
};
