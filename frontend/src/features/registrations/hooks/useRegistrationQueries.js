import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import toast from 'react-hot-toast';
import { registrationFeatureApi } from '../api/registrationApi';
import { registrationKeys, registrationQueryOptions } from '../constants/registrationConstants';

export function useRegister() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: registrationFeatureApi.registerForDrive,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: registrationKeys.all });
      toast.success('Registration submitted');
    },
  });
}

export function useMyRegistrations(userId) {
  return useQuery({
    queryKey: registrationKeys.mine(userId || 'me'),
    queryFn: () => registrationFeatureApi.getMyRegistrations(userId),
    ...registrationQueryOptions,
  });
}

export function useRegistration(registrationId) {
  return useQuery({
    queryKey: registrationKeys.detail(registrationId),
    queryFn: () => registrationFeatureApi.getRegistrationById(registrationId),
    enabled: Boolean(registrationId),
    ...registrationQueryOptions,
  });
}

export function useRegistrationTimeline(registrationId) {
  return useQuery({
    queryKey: registrationKeys.timeline(registrationId),
    queryFn: () => registrationFeatureApi.getRegistrationTimeline(registrationId),
    enabled: Boolean(registrationId),
    ...registrationQueryOptions,
  });
}

export function useRegistrationEligibility(registrationId) {
  return useQuery({
    queryKey: registrationKeys.eligibility(registrationId),
    queryFn: () => registrationFeatureApi.getRegistrationEligibility(registrationId),
    enabled: Boolean(registrationId),
    ...registrationQueryOptions,
  });
}

export function useRegistrationAudit(registrationId) {
  return useQuery({
    queryKey: registrationKeys.audit(registrationId),
    queryFn: () => registrationFeatureApi.getRegistrationAudit(registrationId),
    enabled: Boolean(registrationId),
    ...registrationQueryOptions,
  });
}

export function useRegistrationCommunications(registrationId) {
  return useQuery({
    queryKey: registrationKeys.communications(registrationId),
    queryFn: () => registrationFeatureApi.getRegistrationCommunications(registrationId),
    enabled: Boolean(registrationId),
    ...registrationQueryOptions,
  });
}

export function useApprovals() {
  return useQuery({
    queryKey: registrationKeys.approvals,
    queryFn: registrationFeatureApi.getPendingApprovals,
    ...registrationQueryOptions,
  });
}

export function useUpdateRegistrationStatus() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: registrationFeatureApi.updateRegistrationStatus,
    onSuccess: () => queryClient.invalidateQueries({ queryKey: registrationKeys.all }),
  });
}

export function useApproveRegistration() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ registrationId, payload }) => registrationFeatureApi.approveRegistration(registrationId, payload),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: registrationKeys.approvals });
      toast.success('Registration approved');
    },
  });
}

export function useRejectRegistration() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ registrationId, payload }) => registrationFeatureApi.rejectRegistration(registrationId, payload),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: registrationKeys.approvals });
      toast.success('Registration rejected');
    },
  });
}
