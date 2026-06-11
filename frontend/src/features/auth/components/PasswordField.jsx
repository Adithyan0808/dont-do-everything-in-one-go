import { useMemo, useState } from 'react';
import { Eye, EyeOff } from 'lucide-react';

function PasswordField({ id, label, register, error, value, autoComplete = 'current-password', showStrength = false }) {
  const [visible, setVisible] = useState(false);
  const strength = useMemo(() => {
    if (!value) return 0;
    return [
      value.length >= 8,
      /[A-Z]/.test(value),
      /[a-z]/.test(value),
      /[0-9]/.test(value),
      /[^A-Za-z0-9]/.test(value),
    ].filter(Boolean).length;
  }, [value]);

  return (
    <div>
      <label htmlFor={id} className="block text-sm font-medium text-slate-700">
        {label}
      </label>
      <div className="relative mt-1">
        <input
          id={id}
          type={visible ? 'text' : 'password'}
          autoComplete={autoComplete}
          aria-invalid={Boolean(error)}
          aria-describedby={error ? `${id}-error` : undefined}
          className="block w-full rounded-md border border-slate-300 px-3 py-2 pr-10 text-sm outline-none focus:border-primary focus:ring-2 focus:ring-primary/20"
          {...register(id)}
        />
        <button
          type="button"
          className="absolute inset-y-0 right-2 flex items-center text-slate-500"
          onClick={() => setVisible((current) => !current)}
          aria-label={visible ? 'Hide password' : 'Show password'}
        >
          {visible ? <EyeOff className="h-4 w-4" /> : <Eye className="h-4 w-4" />}
        </button>
      </div>
      {showStrength && (
        <div className="mt-2 flex gap-1" aria-label={`Password strength ${strength} of 5`}>
          {[1, 2, 3, 4, 5].map((item) => (
            <span
              key={item}
              className={`h-1 flex-1 rounded-full ${item <= strength ? 'bg-success' : 'bg-slate-200'}`}
            />
          ))}
        </div>
      )}
      {error && (
        <p id={`${id}-error`} className="mt-1 text-sm text-danger">
          {error.message}
        </p>
      )}
    </div>
  );
}

export default PasswordField;
