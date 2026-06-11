export const activeRegistrationDrives = [
  {
    driveId: 'aws-saa-q3-2026',
    driveName: 'AWS Solutions Architect Associate - Q3 2026',
    vendorName: 'AWS',
    certificationName: 'Solutions Architect Associate',
    registrationWindow: 'Jul 1 - Jul 20, 2026',
    prerequisites: 'Cloud Practitioner recommended',
    seatsRemaining: 32,
    passRate: 82,
  },
  {
    driveId: 'azure-fundamentals-2026',
    driveName: 'Azure Fundamentals Enterprise Wave',
    vendorName: 'Azure',
    certificationName: 'Azure Fundamentals',
    registrationWindow: 'Aug 1 - Aug 18, 2026',
    prerequisites: 'None',
    seatsRemaining: 120,
    passRate: 0,
  },
];

export const mockRegistrations = [
  {
    registrationId: 'REG-2026-0001',
    driveId: 'aws-saa-q3-2026',
    driveName: 'AWS Solutions Architect Associate - Q3 2026',
    vendorName: 'AWS',
    certificationName: 'Solutions Architect Associate',
    registeredDate: '2026-07-02',
    status: 'VoucherAssigned',
    eligibilityResult: 'Passed',
    approvalResult: 'Approved',
    assessmentResult: 'Passed',
    voucherStatus: 'Assigned',
    certificationStatus: 'In Progress',
    nextExamDate: '2026-08-20',
    candidate: {
      employeeId: 'MAV1021',
      fullName: 'Sanjay Menon',
      email: 'sanjay.menon@maverick.com',
      department: 'Cloud',
      businessUnit: 'BFSI',
      location: 'Bengaluru',
      managerEmail: 'priya.nair@maverick.com',
    },
  },
  {
    registrationId: 'REG-2026-0002',
    driveId: 'azure-fundamentals-2026',
    driveName: 'Azure Fundamentals Enterprise Wave',
    vendorName: 'Azure',
    certificationName: 'Azure Fundamentals',
    registeredDate: '2026-08-03',
    status: 'PendingApproval',
    eligibilityResult: 'Warning',
    approvalResult: 'Pending',
    assessmentResult: 'Not Started',
    voucherStatus: 'Not Assigned',
    certificationStatus: 'Not Started',
    candidate: {
      employeeId: 'MAV1044',
      fullName: 'Ananya Krishnan',
      email: 'ananya.krishnan@maverick.com',
      department: 'Security',
      businessUnit: 'Retail',
      location: 'Chennai',
      managerEmail: 'rohit.verma@maverick.com',
    },
  },
  {
    registrationId: 'REG-2026-0003',
    driveId: 'gcp-digital-leader-q2',
    driveName: 'Google Cloud Digital Leader - Q2 Closeout',
    vendorName: 'Google Cloud',
    certificationName: 'Cloud Digital Leader',
    registeredDate: '2026-04-04',
    status: 'Certified',
    eligibilityResult: 'Passed',
    approvalResult: 'Approved',
    assessmentResult: 'Passed',
    voucherStatus: 'Redeemed',
    certificationStatus: 'Certified',
    candidate: {
      employeeId: 'MAV1012',
      fullName: 'Vivek Kumar',
      email: 'vivek.kumar@maverick.com',
      department: 'Data Engineering',
      businessUnit: 'Healthcare',
      location: 'Hyderabad',
      managerEmail: 'divya.iyer@maverick.com',
    },
  },
];

export const mockEligibility = [
  { ruleName: 'Prerequisite Certifications', status: 'Pass', reason: 'AWS Cloud Practitioner found', evidence: 'Credential verified' },
  { ruleName: 'Tenure Requirement', status: 'Pass', reason: 'Employee tenure exceeds 180 days', evidence: 'HR profile' },
  { ruleName: 'Attempt Limits', status: 'Pass', reason: 'No prior failed attempts in current window', evidence: 'Assessment history' },
  { ruleName: 'Training Completion', status: 'Warning', reason: 'Training completion pending manager attestation', evidence: 'LMS pending' },
  { ruleName: 'Budget Availability', status: 'Pass', reason: 'Drive budget has available allocation', evidence: 'Budget ledger' },
];

export const mockTimeline = [
  { id: 't1', event: 'Registration Submitted', timestamp: '2026-07-02 10:08', actor: 'Sanjay Menon', status: 'Completed' },
  { id: 't2', event: 'Eligibility Passed', timestamp: '2026-07-02 10:10', actor: 'Eligibility Engine', status: 'Completed' },
  { id: 't3', event: 'Approval Granted', timestamp: '2026-07-03 13:20', actor: 'Priya Nair', status: 'Completed' },
  { id: 't4', event: 'Voucher Assigned', timestamp: '2026-07-04 09:15', actor: 'Certification Ops', status: 'Completed' },
];

export const mockCommunications = [
  { id: 'c1', channel: 'Email', subject: 'Registration Submitted', timestamp: '2026-07-02 10:09', status: 'Delivered' },
  { id: 'c2', channel: 'Notification', subject: 'Eligibility Passed', timestamp: '2026-07-02 10:10', status: 'Read' },
  { id: 'c3', channel: 'Reminder', subject: 'Schedule your exam', timestamp: '2026-07-10 09:00', status: 'Delivered' },
];

export const mockAudit = [
  { id: 'a1', actor: 'Sanjay Menon', action: 'Submitted registration', before: '{}', after: '{"status":"Pending"}', timestamp: '2026-07-02 10:08' },
  { id: 'a2', actor: 'Eligibility Engine', action: 'Eligibility evaluated', before: '{"status":"Pending"}', after: '{"status":"PendingApproval"}', timestamp: '2026-07-02 10:10' },
  { id: 'a3', actor: 'Priya Nair', action: 'Approved registration', before: '{"approval":"Pending"}', after: '{"approval":"Approved"}', timestamp: '2026-07-03 13:20' },
];

export const mockApprovals = [
  { id: 'APP-1', candidate: 'Ananya Krishnan', drive: 'Azure Fundamentals Enterprise Wave', certification: 'Azure Fundamentals', eligibility: 'Warning', submittedDate: '2026-08-03', slaRemaining: '5h 20m', slaStatus: 'At Risk' },
  { id: 'APP-2', candidate: 'Meera Reddy', drive: 'AWS Solutions Architect Associate - Q3 2026', certification: 'Solutions Architect Associate', eligibility: 'Passed', submittedDate: '2026-07-18', slaRemaining: '18h', slaStatus: 'On Time' },
  { id: 'APP-3', candidate: 'Karthik Raman', drive: 'Security Specialty Drive', certification: 'Security Specialty', eligibility: 'Review', submittedDate: '2026-07-17', slaRemaining: 'Breached', slaStatus: 'Breached' },
];
