export const AUTH_ROLES = {
  admin: 'Admin',
  coordinator: 'Coordinator',
  approver: 'Approver',
  manager: 'Manager',
  candidate: 'Candidate',
  employee: 'Employee',
};

export const AUTH_PERMISSIONS = {
  manageUsers: 'users:manage',
  manageDrives: 'drives:manage',
  manageRegistrations: 'registrations:manage',
  manageVouchers: 'vouchers:manage',
  manageAssessments: 'assessments:manage',
  viewReports: 'reports:view',
  viewAuditLogs: 'audit:view',
  approveRegistrations: 'registrations:approve',
  executeEligibilityDecisions: 'eligibility:decide',
  registerForDrives: 'drives:register',
  viewOwnRegistrations: 'registrations:view-own',
  viewOwnVouchers: 'vouchers:view-own',
};

export const rolePermissions = {
  Admin: [
    AUTH_PERMISSIONS.manageUsers,
    AUTH_PERMISSIONS.manageDrives,
    AUTH_PERMISSIONS.manageRegistrations,
    AUTH_PERMISSIONS.manageVouchers,
    AUTH_PERMISSIONS.viewReports,
    AUTH_PERMISSIONS.viewAuditLogs,
  ],
  Coordinator: [
    AUTH_PERMISSIONS.manageDrives,
    AUTH_PERMISSIONS.manageRegistrations,
    AUTH_PERMISSIONS.manageAssessments,
    AUTH_PERMISSIONS.viewReports,
  ],
  Approver: [
    AUTH_PERMISSIONS.approveRegistrations,
    AUTH_PERMISSIONS.executeEligibilityDecisions,
  ],
  Manager: [
    AUTH_PERMISSIONS.approveRegistrations,
    AUTH_PERMISSIONS.executeEligibilityDecisions,
  ],
  Candidate: [
    AUTH_PERMISSIONS.registerForDrives,
    AUTH_PERMISSIONS.viewOwnRegistrations,
    AUTH_PERMISSIONS.viewOwnVouchers,
  ],
  Employee: [
    AUTH_PERMISSIONS.registerForDrives,
    AUTH_PERMISSIONS.viewOwnRegistrations,
    AUTH_PERMISSIONS.viewOwnVouchers,
  ],
};

export function normalizeRoles(roles) {
  if (!roles) return [];
  return Array.isArray(roles) ? roles : [roles];
}

export function getPermissionsForRoles(roles) {
  return [...new Set(normalizeRoles(roles).flatMap((role) => rolePermissions[role] || []))];
}

export function hasRole(userRoles, allowedRoles = []) {
  const roles = normalizeRoles(userRoles);
  const allowed = normalizeRoles(allowedRoles);
  if (allowed.length === 0) return true;
  return roles.some((role) => allowed.includes(role));
}

export function hasPermission(userRoles, permission) {
  return getPermissionsForRoles(userRoles).includes(permission);
}

export const canCreateDrive = (roles) => hasPermission(roles, AUTH_PERMISSIONS.manageDrives);
export const canEditDrive = (roles) => hasPermission(roles, AUTH_PERMISSIONS.manageDrives);
export const canDeleteDrive = (roles) => normalizeRoles(roles).includes(AUTH_ROLES.admin);
export const canApproveRegistration = (roles) => hasPermission(roles, AUTH_PERMISSIONS.approveRegistrations);
export const canAssignVoucher = (roles) => hasPermission(roles, AUTH_PERMISSIONS.manageVouchers) || hasPermission(roles, AUTH_PERMISSIONS.manageDrives);
export const canViewAuditLogs = (roles) => hasPermission(roles, AUTH_PERMISSIONS.viewAuditLogs);
export const canViewReports = (roles) => hasPermission(roles, AUTH_PERMISSIONS.viewReports);

export function getDefaultRouteForRole(role) {
  if (role === AUTH_ROLES.approver) return '/approvals';
  if (role === AUTH_ROLES.candidate || role === AUTH_ROLES.employee) return '/my-drives';
  return '/dashboard';
}
