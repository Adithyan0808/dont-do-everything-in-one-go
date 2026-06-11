import { useMutation } from '@tanstack/react-query';
import { useNavigate } from 'react-router-dom';
import toast from 'react-hot-toast';
import { registerUser } from '../api/authApi';

export function useRegister() {
  const navigate = useNavigate();

  return useMutation({
    mutationFn: async (payload) => {
      const { confirmPassword, ...request } = payload;
      return registerUser(request);
    },
    onSuccess: () => {
      toast.success('User registered successfully');
      navigate('/login');
    },
    onError: (error) => {
      toast.error(error?.response?.data?.message || 'Registration failed');
    },
  });
}
