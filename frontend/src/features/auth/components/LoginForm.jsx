import { zodResolver } from '@hookform/resolvers/zod';
import { Loader2 } from 'lucide-react';
import { useForm } from 'react-hook-form';
import { Link } from 'react-router-dom';
import { loginSchema } from '../schemas/loginSchema';
import { useLogin } from '../hooks/useLogin';
import PasswordField from './PasswordField';
import RememberMeCheckbox from './RememberMeCheckbox';

function LoginForm() {
  const loginMutation = useLogin();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: zodResolver(loginSchema),
    defaultValues: {
      email: '',
      password: '',
      rememberMe: true,
    },
  });

  return (
    <form className="space-y-4" onSubmit={handleSubmit((values) => loginMutation.mutate(values))}>
      {loginMutation.isError && (
        <div className="rounded-md border border-danger/20 bg-red-50 p-3 text-sm text-red-800" role="alert">
          {loginMutation.error?.response?.data?.message || 'Unable to sign in. Check your credentials and try again.'}
        </div>
      )}

      <div>
        <label htmlFor="email" className="block text-sm font-medium text-slate-700">
          Email
        </label>
        <input
          id="email"
          type="email"
          autoComplete="email"
          autoFocus
          aria-invalid={Boolean(errors.email)}
          aria-describedby={errors.email ? 'email-error' : undefined}
          className="mt-1 block w-full rounded-md border border-slate-300 px-3 py-2 text-sm outline-none focus:border-primary focus:ring-2 focus:ring-primary/20"
          {...register('email')}
        />
        {errors.email && (
          <p id="email-error" className="mt-1 text-sm text-danger">
            {errors.email.message}
          </p>
        )}
      </div>

      <PasswordField id="password" label="Password" register={register} error={errors.password} />

      <div className="flex items-center justify-between">
        <RememberMeCheckbox register={register} />
        <Link className="text-sm font-medium text-primary hover:underline" to="/login">
          Forgot password?
        </Link>
      </div>

      <button
        type="submit"
        disabled={loginMutation.isPending}
        className="inline-flex w-full items-center justify-center rounded-md bg-primary px-4 py-2.5 text-sm font-semibold text-white hover:bg-indigo-700 disabled:cursor-not-allowed disabled:opacity-70"
      >
        {loginMutation.isPending && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
        Sign in
      </button>
    </form>
  );
}

export default LoginForm;
