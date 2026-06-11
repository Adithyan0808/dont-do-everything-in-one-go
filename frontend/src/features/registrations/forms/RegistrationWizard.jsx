import { zodResolver } from '@hookform/resolvers/zod';
import { useMemo, useState } from 'react';
import { FormProvider, useForm, useFormContext } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../hooks/useAuth';
import { registrationWizardSchema } from '../schemas/registrationSchemas';
import { activeRegistrationDrives, mockEligibility } from '../services/registrationMockData';
import { useRegister } from '../hooks/useRegistrationQueries';
import EligibilityPreviewWidget from '../widgets/EligibilityPreviewWidget';

const steps = ['Select Certification Drive', 'Candidate Information', 'Eligibility Preview', 'Exam Preferences', 'Review & Submit'];

function Field({ label, name, type = 'text', readOnly = false }) {
  const methods = useFormContext();
  const error = methods.formState.errors[name]?.message;
  return (
    <label className="block">
      <span className="text-sm font-medium text-slate-700">{label}</span>
      <input
        type={type}
        readOnly={readOnly}
        {...methods.register(name)}
        className="mt-1 w-full rounded-md border border-slate-300 px-3 py-2 text-sm outline-none focus:border-primary focus:ring-2 focus:ring-primary/20 read-only:bg-slate-50"
      />
      {error && <span className="mt-1 block text-xs text-danger">{error}</span>}
    </label>
  );
}

function DriveSelectionStep({ search, setSearch }) {
  const methods = useFormContext();
  const selectedDrive = methods.watch('driveId');
  const drives = useMemo(() => activeRegistrationDrives.filter((drive) => `${drive.driveName} ${drive.vendorName} ${drive.certificationName}`.toLowerCase().includes(search.toLowerCase())), [search]);
  return (
    <div className="space-y-4">
      <input className="w-full rounded-md border border-slate-300 px-3 py-2 text-sm" placeholder="Search active drives" value={search} onChange={(event) => setSearch(event.target.value)} />
      <div className="grid gap-4 lg:grid-cols-2">
        {drives.map((drive) => (
          <button
            key={drive.driveId}
            type="button"
            className={`rounded-card border p-4 text-left shadow-sm ${selectedDrive === drive.driveId ? 'border-primary bg-indigo-50' : 'border-slate-200 bg-white'}`}
            onClick={() => methods.setValue('driveId', drive.driveId, { shouldValidate: true })}
          >
            <p className="font-semibold text-slate-950">{drive.driveName}</p>
            <p className="mt-1 text-sm text-slate-500">{drive.vendorName} · {drive.certificationName}</p>
            <dl className="mt-3 grid grid-cols-2 gap-3 text-sm">
              <div><dt className="text-slate-500">Window</dt><dd>{drive.registrationWindow}</dd></div>
              <div><dt className="text-slate-500">Prerequisites</dt><dd>{drive.prerequisites}</dd></div>
              <div><dt className="text-slate-500">Seats</dt><dd>{drive.seatsRemaining}</dd></div>
              <div><dt className="text-slate-500">Pass Rate</dt><dd>{drive.passRate}%</dd></div>
            </dl>
          </button>
        ))}
      </div>
    </div>
  );
}

function CandidateStep() {
  const methods = useFormContext();
  const values = methods.watch();
  const missing = ['managerEmail', 'department', 'email'].filter((key) => !values[key]);
  return (
    <div className="space-y-4">
      {missing.length > 0 && <div className="rounded-card border border-red-200 bg-red-50 p-3 text-sm text-red-800">Profile incomplete: {missing.join(', ')}</div>}
      <div className="grid gap-4 lg:grid-cols-2">
        <Field label="Employee ID" name="employeeId" readOnly />
        <Field label="Name" name="fullName" readOnly />
        <Field label="Email" name="email" readOnly />
        <Field label="Department" name="department" readOnly />
        <Field label="Business Unit" name="businessUnit" readOnly />
        <Field label="Location" name="location" readOnly />
        <Field label="Manager" name="managerEmail" readOnly />
      </div>
    </div>
  );
}

function EligibilityStep() {
  return <EligibilityPreviewWidget checks={mockEligibility} />;
}

function ExamPreferencesStep() {
  return (
    <div className="grid gap-4 lg:grid-cols-2">
      <Field label="Exam Track" name="examTrack" />
      <Field label="Preferred Date" name="preferredDate" type="date" />
      <Field label="Preferred Time" name="preferredTime" type="time" />
      <Field label="Preferred Time Zone" name="preferredTimeZone" />
      <Field label="Prior Attempts" name="priorAttempts" type="number" />
      <Field label="Special Accommodations" name="specialAccommodations" />
    </div>
  );
}

function ReviewStep() {
  const values = useFormContext().watch();
  const drive = activeRegistrationDrives.find((item) => item.driveId === values.driveId);
  return (
    <div className="grid gap-4 lg:grid-cols-2">
      {[
        ['Drive Summary', drive?.driveName, `${drive?.vendorName} · ${drive?.certificationName}`],
        ['Candidate Summary', values.fullName, `${values.employeeId} · ${values.email}`],
        ['Eligibility Summary', 'Checks completed', 'Warnings require manager attention'],
        ['Exam Preferences', `${values.preferredDate} ${values.preferredTime}`, values.preferredTimeZone],
      ].map(([title, line1, line2]) => (
        <article key={title} className="rounded-card border border-slate-200 p-4">
          <h3 className="font-semibold text-slate-900">{title}</h3>
          <p className="mt-2 text-sm text-slate-600">{line1}</p>
          <p className="text-sm text-slate-500">{line2}</p>
        </article>
      ))}
    </div>
  );
}

const stepComponents = [DriveSelectionStep, CandidateStep, EligibilityStep, ExamPreferencesStep, ReviewStep];

function RegistrationWizard() {
  const [step, setStep] = useState(0);
  const [search, setSearch] = useState('');
  const [successRegistration, setSuccessRegistration] = useState(null);
  const { user } = useAuth();
  const navigate = useNavigate();
  const registerMutation = useRegister();
  const methods = useForm({
    resolver: zodResolver(registrationWizardSchema),
    defaultValues: {
      driveId: '',
      employeeId: user?.employeeId || 'MAV1021',
      fullName: user?.fullName || 'Sanjay Menon',
      email: user?.email || 'sanjay.menon@maverick.com',
      department: user?.department || 'Cloud',
      businessUnit: user?.businessUnit || 'BFSI',
      location: user?.location || 'Bengaluru',
      managerEmail: user?.managerEmail || 'priya.nair@maverick.com',
      examTrack: 'Online Proctored',
      preferredDate: '',
      preferredTime: '',
      preferredTimeZone: 'Asia/Kolkata',
      priorAttempts: 0,
      specialAccommodations: '',
    },
  });
  const Step = stepComponents[step];

  const submit = methods.handleSubmit((values) => {
    registerMutation.mutate(values, {
      onSuccess: (result) => setSuccessRegistration(result),
    });
  });

  return (
    <FormProvider {...methods}>
      <form onSubmit={submit} className="space-y-5">
        <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
          <div className="flex flex-wrap gap-2">
            {steps.map((label, index) => (
              <button key={label} type="button" className={`rounded-full px-3 py-1.5 text-sm font-medium ${index === step ? 'bg-primary text-white' : 'bg-slate-100 text-slate-600'}`} onClick={() => setStep(index)}>
                {index + 1}. {label}
              </button>
            ))}
          </div>
        </section>
        <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
          <h2 className="text-lg font-semibold text-slate-950">{steps[step]}</h2>
          <div className="mt-5"><Step search={search} setSearch={setSearch} /></div>
        </section>
        <div className="flex justify-end gap-2">
          <button type="button" className="rounded-md border border-slate-300 px-3 py-2 text-sm" disabled={step === 0} onClick={() => setStep((current) => current - 1)}>Back</button>
          {step < steps.length - 1 ? (
            <button type="button" className="rounded-md bg-primary px-3 py-2 text-sm font-semibold text-white" onClick={() => setStep((current) => current + 1)}>Next</button>
          ) : (
            <button type="submit" className="rounded-md bg-primary px-3 py-2 text-sm font-semibold text-white" disabled={registerMutation.isPending}>Submit Registration</button>
          )}
        </div>
      </form>
      {successRegistration && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-slate-950/50 p-4">
          <section className="w-full max-w-lg rounded-card bg-white p-6 shadow-xl">
            <h2 className="text-xl font-semibold text-slate-950">Registration Submitted</h2>
            <p className="mt-2 text-sm text-slate-500">Registration Number: {successRegistration.registrationId}</p>
            <p className="mt-4 text-sm text-slate-600">Eligibility, audit event, notification, and SLA tracking have been initiated.</p>
            <button type="button" className="mt-5 rounded-md bg-primary px-3 py-2 text-sm font-semibold text-white" onClick={() => navigate('/my-registrations')}>View My Registrations</button>
          </section>
        </div>
      )}
    </FormProvider>
  );
}

export default RegistrationWizard;
