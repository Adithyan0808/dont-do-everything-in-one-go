import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import toast from 'react-hot-toast';
import { eligibilityFeatureApi } from '../api/eligibilityApi';
import { eligibilityKeys, eligibilityQueryOptions } from '../constants/eligibilityConstants';

export function useCheckEligibility(registrationId) {
  return useQuery({
    queryKey: eligibilityKeys.detail(registrationId),
    queryFn: () => eligibilityFeatureApi.checkEligibility(registrationId),
    enabled: Boolean(registrationId),
    ...eligibilityQueryOptions,
  });
}

export function useEligibilityRules(registrationId) {
  return useQuery({
    queryKey: eligibilityKeys.rules(registrationId),
    queryFn: () => eligibilityFeatureApi.getEligibilityRules(registrationId),
    enabled: Boolean(registrationId),
    ...eligibilityQueryOptions,
  });
}

export function usePendingApprovals() {
  return useQuery({
    queryKey: eligibilityKeys.approvals,
    queryFn: () => eligibilityFeatureApi.getPendingApprovals(),
    ...eligibilityQueryOptions,
  });
}

export function useApproveEligibility() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ registrationId, payload }) => eligibilityFeatureApi.approveEligibility(registrationId, payload),
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: eligibilityKeys.approvals });
      toast.success('Eligibility approved');
    },
  });
}

export function useRejectEligibility() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ registrationId, payload }) => eligibilityFeatureApi.rejectEligibility(registrationId, payload),
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: eligibilityKeys.approvals });
      toast.success('Eligibility rejected');
    },
  });
}

export function useBulkApprove() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ ids }) => eligibilityFeatureApi.bulkApprove(ids),
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: eligibilityKeys.approvals });
      toast.success('Bulk approve completed');
    },
  });
}

export function useApprovalHistory(registrationId) {
  return useQuery({
    queryKey: eligibilityKeys.history(registrationId),
    queryFn: () => eligibilityFeatureApi.getApprovalHistory(registrationId),
    enabled: Boolean(registrationId),
    ...eligibilityQueryOptions,
  });
}
