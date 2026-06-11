import { useQuery } from '@tanstack/react-query';

export function useApi(queryKey, queryFn, options = {}) {
  return useQuery({
    queryKey,
    queryFn,
    ...options,
  });
}
