import { QueryCache, QueryClient } from '@tanstack/react-query';
import toast from 'react-hot-toast';

export const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: 1,
      staleTime: 5 * 60 * 1000,
      gcTime: 10 * 60 * 1000,
      refetchOnWindowFocus: false,
    },
    mutations: {
      retry: 0,
      onError: (error) => {
        const message = error?.response?.data?.message || error?.message || 'Request failed';
        toast.error(message);
      },
    },
  },
  queryCache: new QueryCache({
    onError: (error) => {
      const message = error?.response?.data?.message || error?.message || 'Unable to load data';
      toast.error(message);
    },
  }),
});
