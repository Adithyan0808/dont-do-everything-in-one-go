import toast from 'react-hot-toast';

export function showSuccessToast(message) {
  toast.success(message);
}

export function showErrorToast(message) {
  toast.error(message);
}
