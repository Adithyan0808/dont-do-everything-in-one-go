import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import toast from 'react-hot-toast';
import { driveFeatureApi } from '../api/driveApi';
import { driveKeys, driveQueryOptions } from '../constants/driveConstants';

export function useDrives(filters = {}) {
  return useQuery({
    queryKey: [...driveKeys.all, filters],
    queryFn: driveFeatureApi.getDrives,
    ...driveQueryOptions,
  });
}

export function useDrive(driveId) {
  return useQuery({
    queryKey: driveKeys.detail(driveId),
    queryFn: () => driveFeatureApi.getDrive(driveId),
    enabled: Boolean(driveId),
    ...driveQueryOptions,
  });
}

export function useDriveDashboard(driveId) {
  return useQuery({
    queryKey: driveKeys.dashboard(driveId),
    queryFn: () => driveFeatureApi.getDriveDashboard(driveId),
    enabled: Boolean(driveId),
    ...driveQueryOptions,
  });
}

export function useDriveRegistrations(driveId) {
  return useQuery({
    queryKey: driveKeys.registrations(driveId),
    queryFn: () => driveFeatureApi.getDriveRegistrations(driveId),
    enabled: Boolean(driveId),
    ...driveQueryOptions,
  });
}

export function useDriveAnalytics(driveId) {
  return useQuery({
    queryKey: driveKeys.analytics(driveId),
    queryFn: () => driveFeatureApi.getDriveAnalytics(driveId),
    enabled: Boolean(driveId),
    ...driveQueryOptions,
  });
}

export function useDriveVouchers(driveId) {
  return useQuery({
    queryKey: driveKeys.vouchers(driveId),
    queryFn: () => driveFeatureApi.getDriveVouchers(driveId),
    enabled: Boolean(driveId),
    ...driveQueryOptions,
  });
}

function useDriveMutation(mutationFn, successMessage) {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: driveKeys.all });
      toast.success(successMessage);
    },
  });
}

export function useCreateDrive() {
  return useDriveMutation(driveFeatureApi.createDrive, 'Drive saved successfully');
}

export function useUpdateDrive() {
  return useDriveMutation(({ driveId, payload }) => driveFeatureApi.updateDrive(driveId, payload), 'Drive updated successfully');
}

export function useActivateDrive() {
  return useDriveMutation(driveFeatureApi.activateDrive, 'Drive activated');
}

export function useCloseDrive() {
  return useDriveMutation(driveFeatureApi.closeDrive, 'Drive closed');
}

export function useArchiveDrive() {
  return useDriveMutation(driveFeatureApi.archiveDrive, 'Drive archived');
}
