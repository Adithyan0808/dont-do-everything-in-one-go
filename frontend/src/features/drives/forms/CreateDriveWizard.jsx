import { zodResolver } from '@hookform/resolvers/zod';
import { useEffect, useMemo, useState } from 'react';
import { FormProvider, useForm, useFormContext } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';
import toast from 'react-hot-toast';
import { DISTRIBUTION_STRATEGIES, VENDOR_CATALOG } from '../constants/driveConstants';
import { createDriveSchema } from '../schemas/driveSchemas';
import { useCreateDrive } from '../hooks/useDriveQueries';
import { buildDriveCode, formatCurrency } from '../utils/driveFormatters';

const DRAFT_KEY = 'mch_drive_draft';

const steps = ['Program Details', 'Timeline', 'Eligibility Rules', 'Voucher Configuration', 'Budget Configuration', 'Review & Publish'];

const defaultValues = {
  driveName: '',
  driveCode: '',
  vendorName: 'AWS',
  certificationName: 'Solutions Architect Associate',
  certificationLevel: 'Associate',
  sponsorName: '',
  businessUnit: 'BFSI',
  description: '',
  objectives: '',
  registrationStartDate: '',
  registrationEndDate: '',
  examWindowStartDate: '',
  examWindowEndDate: '',
  closureDate: '',
  prerequisiteCertification: '',
  managerApprovalRequired: true,
  attemptLimit: 2,
  tenureRequiredDays: 180,
  minimumExperienceMonths: 12,
  trainingCompletionRequired: true,
  departmentRestrictions: '',
  businessUnitRestrictions: '',
  locationRestrictions: '',
  voucherVendor: 'AWS',
  voucherValue: 25000,
  voucherQuantity: 100,
  voucherExpiry: '',
  voucherDistributionStrategy: 'Approval Based',
  totalBudget: 2500000,
  trainingBudget: 400000,
  voucherBudget: 2000000,
  contingencyBudget: 100000,
  approvalThreshold: 500000,
};

function Field({ label, name, type = 'text', children }) {
  const methods = useFormContext();
  const error = methods.formState.errors[name]?.message;
  return (
    <label className="block">
      <span className="text-sm font-medium text-slate-700">{label}</span>
      {children || (
        <input
          type={type}
          {...methods.register(name)}
          className="mt-1 w-full rounded-md border border-slate-300 px-3 py-2 text-sm outline-none focus:border-primary focus:ring-2 focus:ring-primary/20"
        />
      )}
      {error && <span className="mt-1 block text-xs text-danger">{error}</span>}
    </label>
  );
}

function ProgramDetailsStep() {
  const methods = useFormContext();
  const vendor = methods.watch('vendorName');
  const certification = methods.watch('certificationName');

  useEffect(() => {
    methods.setValue('driveCode', buildDriveCode(vendor, certification), { shouldValidate: true });
  }, [certification, methods, vendor]);

  return (
    <div className="grid gap-4 lg:grid-cols-2">
      <Field label="Drive Name" name="driveName" />
      <Field label="Drive Code" name="driveCode" />
      <Field label="Vendor" name="vendorName">
        <select {...methods.register('vendorName')} className="mt-1 w-full rounded-md border border-slate-300 px-3 py-2 text-sm">
          {Object.keys(VENDOR_CATALOG).map((vendorName) => <option key={vendorName}>{vendorName}</option>)}
        </select>
      </Field>
      <Field label="Certification" name="certificationName">
        <select {...methods.register('certificationName')} className="mt-1 w-full rounded-md border border-slate-300 px-3 py-2 text-sm">
          {(VENDOR_CATALOG[vendor] || []).map((option) => <option key={option}>{option}</option>)}
        </select>
      </Field>
      <Field label="Certification Level" name="certificationLevel" />
      <Field label="Sponsor" name="sponsorName" />
      <Field label="Business Unit" name="businessUnit" />
      <Field label="Description" name="description" />
      <Field label="Objectives" name="objectives" />
    </div>
  );
}

function TimelineStep() {
  return (
    <div className="grid gap-4 lg:grid-cols-2">
      <Field label="Registration Start" name="registrationStartDate" type="date" />
      <Field label="Registration End" name="registrationEndDate" type="date" />
      <Field label="Exam Window Start" name="examWindowStartDate" type="date" />
      <Field label="Exam Window End" name="examWindowEndDate" type="date" />
      <Field label="Drive Closure Date" name="closureDate" type="date" />
    </div>
  );
}

function EligibilityStep() {
  const methods = useFormContext();
  const values = methods.watch();
  return (
    <div className="grid gap-5 lg:grid-cols-[1fr_320px]">
      <div className="grid gap-4 lg:grid-cols-2">
        <Field label="Prerequisite Certification" name="prerequisiteCertification" />
        <Field label="Attempt Limit" name="attemptLimit" type="number" />
        <Field label="Tenure Requirement Days" name="tenureRequiredDays" type="number" />
        <Field label="Minimum Experience Months" name="minimumExperienceMonths" type="number" />
        <Field label="Department Restrictions" name="departmentRestrictions" />
        <Field label="Business Unit Restrictions" name="businessUnitRestrictions" />
        <Field label="Location Restrictions" name="locationRestrictions" />
        <label className="flex items-center gap-2 text-sm text-slate-700">
          <input type="checkbox" {...methods.register('managerApprovalRequired')} /> Manager Approval Required
        </label>
        <label className="flex items-center gap-2 text-sm text-slate-700">
          <input type="checkbox" {...methods.register('trainingCompletionRequired')} /> Training Completion Required
        </label>
      </div>
      <aside className="rounded-card border border-slate-200 bg-slate-50 p-4">
        <h3 className="font-semibold text-slate-900">Eligibility Preview</h3>
        <ul className="mt-3 space-y-2 text-sm text-slate-600">
          <li>Attempts allowed: {values.attemptLimit}</li>
          <li>Tenure required: {values.tenureRequiredDays} days</li>
          <li>Manager approval: {values.managerApprovalRequired ? 'Required' : 'Not required'}</li>
          <li>Training completion: {values.trainingCompletionRequired ? 'Required' : 'Optional'}</li>
        </ul>
      </aside>
    </div>
  );
}

function VoucherStep() {
  const methods = useFormContext();
  return (
    <div className="grid gap-4 lg:grid-cols-2">
      <Field label="Voucher Vendor" name="voucherVendor" />
      <Field label="Voucher Value" name="voucherValue" type="number" />
      <Field label="Voucher Quantity" name="voucherQuantity" type="number" />
      <Field label="Voucher Expiry" name="voucherExpiry" type="date" />
      <Field label="Distribution Strategy" name="voucherDistributionStrategy">
        <select {...methods.register('voucherDistributionStrategy')} className="mt-1 w-full rounded-md border border-slate-300 px-3 py-2 text-sm">
          {DISTRIBUTION_STRATEGIES.map((strategy) => <option key={strategy}>{strategy}</option>)}
        </select>
      </Field>
    </div>
  );
}

function BudgetStep() {
  const methods = useFormContext();
  const values = methods.watch();
  const projectedCost = Number(values.trainingBudget || 0) + Number(values.voucherBudget || 0) + Number(values.contingencyBudget || 0);
  const costPerCandidate = values.voucherQuantity ? projectedCost / Number(values.voucherQuantity) : 0;
  const forecast = values.totalBudget ? Math.round((projectedCost / Number(values.totalBudget)) * 100) : 0;
  return (
    <div className="grid gap-5 lg:grid-cols-[1fr_320px]">
      <div className="grid gap-4 lg:grid-cols-2">
        <Field label="Total Budget" name="totalBudget" type="number" />
        <Field label="Training Budget" name="trainingBudget" type="number" />
        <Field label="Voucher Budget" name="voucherBudget" type="number" />
        <Field label="Contingency Budget" name="contingencyBudget" type="number" />
        <Field label="Approval Threshold" name="approvalThreshold" type="number" />
      </div>
      <aside className="rounded-card border border-slate-200 bg-slate-50 p-4">
        <h3 className="font-semibold text-slate-900">Budget Forecast</h3>
        <dl className="mt-3 space-y-2 text-sm">
          <div className="flex justify-between"><dt>Projected Cost</dt><dd>{formatCurrency(projectedCost)}</dd></div>
          <div className="flex justify-between"><dt>Cost Per Candidate</dt><dd>{formatCurrency(costPerCandidate)}</dd></div>
          <div className="flex justify-between"><dt>Utilization Forecast</dt><dd>{forecast}%</dd></div>
        </dl>
      </aside>
    </div>
  );
}

function ReviewStep() {
  const values = useFormContext().watch();
  const sections = [
    ['Drive Summary', `${values.driveName} (${values.driveCode})`, `${values.vendorName} · ${values.certificationName}`],
    ['Timeline Summary', `${values.registrationStartDate} to ${values.registrationEndDate}`, `${values.examWindowStartDate} to ${values.examWindowEndDate}`],
    ['Voucher Summary', `${values.voucherQuantity} vouchers`, `${values.voucherDistributionStrategy}`],
    ['Budget Summary', formatCurrency(values.totalBudget), `Threshold ${formatCurrency(values.approvalThreshold)}`],
    ['Eligibility Summary', `${values.attemptLimit} attempts`, `${values.tenureRequiredDays} tenure days`],
  ];
  return (
    <div className="grid gap-4 lg:grid-cols-2">
      {sections.map(([title, line1, line2]) => (
        <article key={title} className="rounded-card border border-slate-200 p-4">
          <h3 className="font-semibold text-slate-900">{title}</h3>
          <p className="mt-2 text-sm text-slate-600">{line1}</p>
          <p className="text-sm text-slate-500">{line2}</p>
        </article>
      ))}
    </div>
  );
}

const stepComponents = [ProgramDetailsStep, TimelineStep, EligibilityStep, VoucherStep, BudgetStep, ReviewStep];

function CreateDriveWizard() {
  const [step, setStep] = useState(0);
  const navigate = useNavigate();
  const createDrive = useCreateDrive();
  const savedDraft = useMemo(() => JSON.parse(localStorage.getItem(DRAFT_KEY) || 'null'), []);
  const methods = useForm({
    resolver: zodResolver(createDriveSchema),
    defaultValues: savedDraft || defaultValues,
    mode: 'onBlur',
  });
  const Step = stepComponents[step];

  const saveDraft = () => {
    localStorage.setItem(DRAFT_KEY, JSON.stringify(methods.getValues()));
    toast.success('Draft saved');
  };

  useEffect(() => {
    const interval = setInterval(() => localStorage.setItem(DRAFT_KEY, JSON.stringify(methods.getValues())), 30000);
    return () => clearInterval(interval);
  }, [methods]);

  const submit = methods.handleSubmit((values) => {
    createDrive.mutate(values, {
      onSuccess: () => {
        localStorage.removeItem(DRAFT_KEY);
        navigate('/drives');
      },
    });
  });

  return (
    <FormProvider {...methods}>
      <form onSubmit={submit} className="space-y-5">
        <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
          <div className="flex flex-wrap gap-2">
            {steps.map((label, index) => (
              <button
                type="button"
                key={label}
                className={`rounded-full px-3 py-1.5 text-sm font-medium ${index === step ? 'bg-primary text-white' : 'bg-slate-100 text-slate-600'}`}
                onClick={() => setStep(index)}
              >
                {index + 1}. {label}
              </button>
            ))}
          </div>
        </section>

        <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
          <h2 className="text-lg font-semibold text-slate-950">{steps[step]}</h2>
          <div className="mt-5">
            <Step />
          </div>
        </section>

        <div className="flex flex-wrap items-center justify-between gap-3">
          <div className="flex gap-2">
            <button type="button" className="rounded-md border border-slate-300 px-3 py-2 text-sm" onClick={saveDraft}>Save Draft</button>
            <button type="button" className="rounded-md border border-red-200 px-3 py-2 text-sm text-red-700" onClick={() => { localStorage.removeItem(DRAFT_KEY); methods.reset(defaultValues); }}>Discard Draft</button>
          </div>
          <div className="flex gap-2">
            <button type="button" className="rounded-md border border-slate-300 px-3 py-2 text-sm" disabled={step === 0} onClick={() => setStep((current) => current - 1)}>Back</button>
            {step < steps.length - 1 ? (
              <button type="button" className="rounded-md bg-primary px-3 py-2 text-sm font-semibold text-white" onClick={() => setStep((current) => current + 1)}>Next</button>
            ) : (
              <button type="submit" className="rounded-md bg-primary px-3 py-2 text-sm font-semibold text-white" disabled={createDrive.isPending}>Publish Drive</button>
            )}
          </div>
        </div>
      </form>
    </FormProvider>
  );
}

export default CreateDriveWizard;
