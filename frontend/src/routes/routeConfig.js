import {
  BarChart3,
  CheckSquare,
  ClipboardList,
  FileBarChart,
  Gauge,
  ShieldCheck,
  Ticket,
} from 'lucide-react';

export const routeRoles = {
  all: ['Admin', 'Coordinator', 'Approver', 'Candidate', 'Employee'],
  admin: ['Admin'],
  coordinator: ['Admin', 'Coordinator'],
  approver: ['Admin', 'Approver', 'Manager'],
  candidate: ['Admin', 'Candidate', 'Employee'],
};

export const routeConfig = [
  {
    path: '/dashboard',
    label: 'Dashboard',
    group: 'Workspace',
    icon: Gauge,
    roles: routeRoles.all,
    breadcrumb: ['Dashboard'],
    menu: true,
  },
  {
    path: '/my-drives',
    label: 'My Drives',
    group: 'Workspace',
    icon: ClipboardList,
    roles: routeRoles.candidate,
    breadcrumb: ['Dashboard', 'My Drives'],
    menu: true,
  },
  {
    path: '/drives',
    label: 'Drives',
    group: 'Management',
    icon: ClipboardList,
    roles: routeRoles.coordinator,
    breadcrumb: ['Dashboard', 'Drives'],
    menu: true,
  },
  {
    path: '/approvals',
    label: 'Approvals',
    group: 'Approvals',
    icon: CheckSquare,
    roles: routeRoles.approver,
    breadcrumb: ['Dashboard', 'Approvals'],
    menu: true,
  },
  {
    path: '/vouchers/pool',
    label: 'Voucher Pool',
    group: 'Management',
    icon: Ticket,
    roles: routeRoles.coordinator,
    breadcrumb: ['Dashboard', 'Vouchers'],
    menu: true,
  },
  {
    path: '/my-vouchers',
    label: 'My Vouchers',
    group: 'Workspace',
    icon: Ticket,
    roles: routeRoles.candidate,
    breadcrumb: ['Dashboard', 'My Vouchers'],
    menu: true,
  },
  {
    path: '/reports',
    label: 'Reports',
    group: 'Insights',
    icon: FileBarChart,
    roles: routeRoles.coordinator,
    breadcrumb: ['Dashboard', 'Reports'],
    menu: true,
  },
  {
    path: '/assessments/drive/:driveId',
    label: 'Assessments',
    group: 'Insights',
    icon: BarChart3,
    roles: routeRoles.coordinator,
    breadcrumb: ['Dashboard', 'Assessments'],
    menu: false,
  },
  {
    path: '/eligibility/:registrationId',
    label: 'Eligibility',
    group: 'Approvals',
    icon: ShieldCheck,
    roles: routeRoles.approver,
    breadcrumb: ['Dashboard', 'Eligibility'],
    menu: false,
  },
];

export function getRouteMetadata(pathname) {
  return routeConfig.find((route) => {
    const pattern = route.path.replace(/:[^/]+/g, '[^/]+');
    return new RegExp(`^${pattern}$`).test(pathname);
  });
}
