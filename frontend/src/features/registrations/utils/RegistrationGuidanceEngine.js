export function getRegistrationGuidance(status) {
  const guidance = {
    Pending: 'Registration received and awaiting eligibility evaluation.',
    EligibilityCheck: 'Eligibility checks are currently running.',
    PendingApproval: 'Awaiting manager approval.',
    Approved: 'Approval granted. Schedule your certification exam.',
    Rejected: 'Registration was rejected. Review comments for next steps.',
    ExamScheduled: 'Attend exam on scheduled date.',
    ExamTaken: 'Exam completion recorded. Awaiting result submission.',
    ResultSubmitted: 'Result submitted and under verification.',
    Passed: 'Assessment passed. Voucher or certification completion is next.',
    Failed: 'Retake available after cooldown period.',
    VoucherAssigned: 'Voucher assigned. Redeem it before expiry.',
    VoucherRedeemed: 'Voucher redeemed. Certification completion is in progress.',
    Certified: 'Certification completed.',
    Ineligible: 'Eligibility failed. Review rule results.',
    Cancelled: 'Registration was cancelled.',
  };
  return guidance[status] || 'Track this registration for the next operational update.';
}
