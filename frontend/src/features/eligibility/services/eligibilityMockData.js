export const mockRuleResults = [
  {
    ruleId: 'tenure',
    ruleName: 'Tenure Eligibility',
    status: 'Passed',
    reason: 'Employee tenure 420 days >= required 90 days',
    severity: 'Low',
    evidence: 'HR profile',
    timestamp: '2026-07-02 10:00',
  },
  {
    ruleId: 'training',
    ruleName: 'Training Completion',
    status: 'Warning',
    reason: 'Training completion pending manager attestation',
    severity: 'Medium',
    evidence: 'LMS pending',
    timestamp: '2026-07-02 10:01',
  },
  {
    ruleId: 'manager',
    ruleName: 'Manager Approval',
    status: 'Passed',
    reason: 'Manager approved via HR system',
    severity: 'High',
    evidence: 'Approval record',
    timestamp: '2026-07-02 10:03',
  },
  {
    ruleId: 'attempts',
    ruleName: 'Attempt Limits',
    status: 'Passed',
    reason: 'No prior failed attempts in window',
    severity: 'Low',
    evidence: 'Assessment history',
    timestamp: '2026-07-02 10:02',
  },
  {
    ruleId: 'budget',
    ruleName: 'Budget Validation',
    status: 'Passed',
    reason: 'Drive budget available',
    severity: 'Medium',
    evidence: 'Budget ledger',
    timestamp: '2026-07-02 10:04',
  },
  {
    ruleId: 'department',
    ruleName: 'Department Eligibility',
    status: 'Passed',
    reason: 'Department allowed for this drive',
    severity: 'Low',
    evidence: 'Drive config',
    timestamp: '2026-07-02 10:05',
  },
  {
    ruleId: 'prereq',
    ruleName: 'Prerequisite Certification',
    status: 'Passed',
    reason: 'AWS Cloud Practitioner found',
    severity: 'Low',
    evidence: 'Credential verified',
    timestamp: '2026-07-02 10:06',
  },
];

export const mockEligibilityFor = (registrationId) => ({
  registrationId: registrationId || 'REG-2026-0002',
  driveId: 'aws-saa-q3-2026',
  userId: 'MAV1021',
  overallDecision: mockRuleResults.some((r) => r.status === 'Failed')
    ? 'Rejected'
    : mockRuleResults.some((r) => r.status === 'Warning')
    ? 'ConditionallyEligible'
    : 'Eligible',
  evaluationDate: '2026-07-02T10:10:00Z',
  approver: null,
  decisionDate: null,
  rules: mockRuleResults,
});

export const mockPendingApprovals = [
  {
    registrationId: 'REG-2026-0002',
    candidate: 'Ananya Krishnan',
    employeeId: 'MAV1044',
    department: 'Security',
    businessUnit: 'Retail',
    certification: 'Azure Fundamentals',
    drive: 'Azure Fundamentals Enterprise Wave',
    submittedDate: '2026-08-03',
    slaRemaining: '5h 20m',
    eligibility: 'Warning',
    priority: 'High',
  },
  {
    registrationId: 'REG-2026-0004',
    candidate: 'Meera Reddy',
    employeeId: 'MAV1098',
    department: 'Cloud',
    businessUnit: 'BFSI',
    certification: 'Solutions Architect Associate',
    drive: 'AWS Solutions Architect Associate - Q3 2026',
    submittedDate: '2026-07-18',
    slaRemaining: '18h',
    eligibility: 'Passed',
    priority: 'Medium',
  },
];

export const mockApprovalHistory = [
  {
    id: 'H-1',
    registrationId: 'REG-2026-0001',
    approver: 'Priya Nair',
    decision: 'Approved',
    reason: 'Met all criteria',
    timestamp: '2026-07-03T13:20:00Z',
  },
];
