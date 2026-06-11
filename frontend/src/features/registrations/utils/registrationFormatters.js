export const formatDate = (value) => value || '-';

export function calculateRegistrationMetrics(registrations) {
  return {
    totalRegistrations: registrations.length,
    pendingApprovals: registrations.filter((item) => item.status === 'PendingApproval').length,
    scheduledExams: registrations.filter((item) => item.status === 'ExamScheduled').length,
    passedCertifications: registrations.filter((item) => ['Passed', 'VoucherAssigned', 'VoucherRedeemed', 'Certified'].includes(item.status)).length,
    assignedVouchers: registrations.filter((item) => ['VoucherAssigned', 'VoucherRedeemed', 'Certified'].includes(item.status)).length,
    completedCertifications: registrations.filter((item) => item.status === 'Certified').length,
  };
}
