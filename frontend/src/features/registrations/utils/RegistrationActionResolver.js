export function resolveRegistrationAction(registration) {
  const status = registration?.status;
  if (status === 'Approved') return { label: 'Schedule Exam', to: `/assessments/submit/${registration.registrationId}` };
  if (status === 'ExamScheduled') return { label: 'Submit Result', to: `/assessments/submit/${registration.registrationId}` };
  if (status === 'Passed' || status === 'VoucherAssigned') return { label: 'View Voucher', to: '/my-vouchers' };
  if (status === 'Certified') return { label: 'Download Certificate', to: `/registrations/${registration.registrationId}` };
  if (status === 'Failed') return { label: 'Retake Options', to: `/registrations/${registration.registrationId}` };
  if (status === 'Ineligible') return { label: 'View Eligibility Failure', to: `/registrations/${registration.registrationId}` };
  if (status === 'PendingApproval') return { label: 'Track Approval', to: `/registrations/${registration.registrationId}` };
  return { label: 'View Details', to: `/registrations/${registration?.registrationId}` };
}
