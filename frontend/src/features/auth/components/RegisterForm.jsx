import { zodResolver } from '@hookform/resolvers/zod';
import { Loader2 } from 'lucide-react';
import { useForm } from 'react-hook-form';
import { registerSchema } from '../schemas/registerSchema';
import { useRegister } from '../hooks/useRegister';
import { useAuth } from '../../../hooks/useAuth';
import PasswordField from './PasswordField';

const departments = ['Cloud', 'Data Engineering', 'Security', 'Platform Engineering'];
const businessUnits = ['BFSI', 'Retail', 'Healthcare'];
const roles = ['Admin', 'Coordinator', 'Approver', 'Candidate'];

function RegisterForm() {
  const { role: currentRole } = useAuth();
  const isAdmin = currentRole === 'Admin';
  const registerMutation = useRegister();
  const {
    register,
    handleSubmit,
    watch,
    formState: { errors },
  } = useForm({
    resolver: zodResolver(registerSchema),
    defaultValues: {
      employeeId: '',
      fullName: '',
      email: '',
      department: '',
      businessUnit: '',
      location: '',
      password: '',
      confirmPassword: '',
      role: isAdmin ? 'Candidate' : 'Candidate',
    },
  });

  return (
    <form className="space-y-4" onSubmit={handleSubmit((values) => registerMutation.mutate(values))}>
      {registerMutation.isError && (
        <div className="rounded-md border border-danger/20 bg-red-50 p-3 text-sm text-red-800" role="alert">
          {registerMutation.error?.response?.data?.message || 'Unable to register user.'}
        </div>
      )}

      <div className="grid gap-4 sm:grid-cols-2">
        <TextField id="employeeId" label="Employee ID" register={register} error={errors.employeeId} />
        <TextField id="fullName" label="Full Name" register={register} error={errors.fullName} />
      </div>
      <TextField id="email" label="Email" type="email" register={register} error={errors.email} />
      <div className="grid gap-4 sm:grid-cols-2">
        <SelectField id="department" label="Department" options={departments} register={register} error={errors.department} />
        <SelectField id="businessUnit" label="Business Unit" options={businessUnits} register={register} error={errors.businessUnit} />
      </div>
      <TextField id="location" label="Location" register={register} error={errors.location} />
      <SelectField id="role" label="Role" options={isAdmin ? roles : ['Candidate']} register={register} error={errors.role} disabled={!isAdmin} />
      <PasswordField
        id="password"
        label="Password"
        register={register}
        error={errors.password}
        value={watch('password')}
        autoComplete="new-password"
        showStrength
      />
      <PasswordField
        id="confirmPassword"
        label="Confirm Password"
        register={register}
        error={errors.confirmPassword}
        value={watch('confirmPassword')}
        autoComplete="new-password"
      />

      <button
        type="submit"
        disabled={registerMutation.isPending}
        className="inline-flex w-full items-center justify-center rounded-md bg-primary px-4 py-2.5 text-sm font-semibold text-white hover:bg-indigo-700 disabled:cursor-not-allowed disabled:opacity-70"
      >
        {registerMutation.isPending && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
        Create account
      </button>
    </form>
  );
}

function TextField({ id, label, type = 'text', register, error }) {
  return (
    <div>
      <label htmlFor={id} className="block text-sm font-medium text-slate-700">
        {label}
      </label>
      <input
        id={id}
        type={type}
        aria-invalid={Boolean(error)}
        aria-describedby={error ? `${id}-error` : undefined}
        className="mt-1 block w-full rounded-md border border-slate-300 px-3 py-2 text-sm outline-none focus:border-primary focus:ring-2 focus:ring-primary/20"
        {...register(id)}
      />
      {error && (
        <p id={`${id}-error`} className="mt-1 text-sm text-danger">
          {error.message}
        </p>
      )}
    </div>
  );
}

function SelectField({ id, label, options, register, error, disabled = false }) {
  return (
    <div>
      <label htmlFor={id} className="block text-sm font-medium text-slate-700">
        {label}
      </label>
      <select
        id={id}
        disabled={disabled}
        aria-invalid={Boolean(error)}
        aria-describedby={error ? `${id}-error` : undefined}
        className="mt-1 block w-full rounded-md border border-slate-300 px-3 py-2 text-sm outline-none focus:border-primary focus:ring-2 focus:ring-primary/20 disabled:bg-slate-100"
        {...register(id)}
      >
        <option value="">Select {label}</option>
        {options.map((option) => (
          <option key={option} value={option}>
            {option}
          </option>
        ))}
      </select>
      {error && (
        <p id={`${id}-error`} className="mt-1 text-sm text-danger">
          {error.message}
        </p>
      )}
    </div>
  );
}

export default RegisterForm;
