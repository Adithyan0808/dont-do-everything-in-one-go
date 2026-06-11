import { driveApi as sharedDriveApi } from '../../../api/driveApi';
import { mockDriveAnalytics, mockDriveRegistrations, mockDrives, mockDriveVouchers } from '../services/driveMockData';

const unwrap = (response) => response.data?.data ?? response.data;

async function withFallback(request, fallback) {
  try {
    return unwrap(await request());
  } catch {
    return fallback;
  }
}

const findDrive = (driveId) => mockDrives.find((drive) => drive.driveId === driveId) || mockDrives[0];

export const driveFeatureApi = {
  getDrives: () => withFallback(sharedDriveApi.getAll, mockDrives),
  getDrive: (driveId) => withFallback(() => sharedDriveApi.getById(driveId), findDrive(driveId)),
  createDrive: (payload) => withFallback(() => sharedDriveApi.create(payload), { ...payload, driveId: payload.driveCode || crypto.randomUUID(), status: payload.publish ? 'Active' : 'Draft' }),
  updateDrive: (driveId, payload) => withFallback(() => sharedDriveApi.update(driveId, payload), { ...findDrive(driveId), ...payload }),
  activateDrive: (driveId) => withFallback(() => sharedDriveApi.updateStatus(driveId, 'Active'), { ...findDrive(driveId), status: 'Active' }),
  closeDrive: (driveId) => withFallback(() => sharedDriveApi.updateStatus(driveId, 'Closed'), { ...findDrive(driveId), status: 'Closed' }),
  archiveDrive: (driveId) => withFallback(() => sharedDriveApi.updateStatus(driveId, 'Archived'), { ...findDrive(driveId), status: 'Archived' }),
  getDriveDashboard: (driveId) => withFallback(() => sharedDriveApi.getDashboard(driveId), findDrive(driveId)),
  getDriveRegistrations: () => withFallback(() => Promise.reject(), mockDriveRegistrations),
  getDriveAnalytics: () => withFallback(() => Promise.reject(), mockDriveAnalytics),
  getDriveVouchers: () => withFallback(() => Promise.reject(), mockDriveVouchers),
};
