import RegistrationWizard from '../forms/RegistrationWizard';

function RegisterForDrivePage() {
  return (
    <div className="space-y-5">
      <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
        <h1 className="text-2xl font-semibold text-slate-950">Register for Certification Drive</h1>
        <p className="mt-1 text-sm text-slate-500">Enroll in an active certification drive and track eligibility, approvals, exam preferences, and next steps.</p>
      </section>
      <RegistrationWizard />
    </div>
  );
}

export default RegisterForDrivePage;
