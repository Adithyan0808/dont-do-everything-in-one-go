export function getEligibilityGuidance(decision) {
  switch (decision) {
    case 'Eligible':
      return 'Candidate satisfies all certification drive requirements.';
    case 'ConditionallyEligible':
      return 'Candidate meets most requirements; some conditions require attention before proceeding.';
    case 'PendingApproval':
      return 'Manager approval is required before this registration can proceed.';
    case 'Rejected':
      return 'Candidate does not meet eligibility requirements. Review the failing rules for details.';
    case 'Ineligible':
      return 'Candidate is not eligible for this drive.';
    default:
      return 'Eligibility decision is not available.';
  }
}

export default getEligibilityGuidance;
