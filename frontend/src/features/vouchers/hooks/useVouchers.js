import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { voucherApi } from '../../../api/voucherApi';

const unwrap = (response) => response.data?.data ?? response.data;

export function useDriveVouchers(driveId) {
  return useQuery({
    queryKey: ['vouchers', 'drive', driveId],
    queryFn: async () => unwrap(await voucherApi.getByDrive(driveId)),
    enabled: Boolean(driveId),
  });
}

export function useVoucherStatus(voucherId) {
  return useQuery({
    queryKey: ['voucher', voucherId],
    queryFn: async () => unwrap(await voucherApi.getStatus(voucherId)),
    enabled: Boolean(voucherId),
  });
}

export function useAssignVoucher() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: async (payload) => unwrap(await voucherApi.assign(payload)),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ['vouchers'] }),
  });
}
