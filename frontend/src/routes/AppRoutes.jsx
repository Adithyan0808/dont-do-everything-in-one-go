import { lazy, Suspense } from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import PageLoader from '../components/common/PageLoader';
import ProtectedRoute from '../components/layout/ProtectedRoute';
import RoleGuard from '../components/layout/RoleGuard';
import MainLayout from '../layouts/MainLayout';
import { routeRoles } from './routeConfig';

const LoginPage = lazy(() => import('../features/auth/pages/LoginPage'));
const RegisterPage = lazy(() => import('../features/auth/pages/RegisterPage'));
const UnauthorizedPage = lazy(() => import('../pages/UnauthorizedPage'));
const NotFoundPage = lazy(() => import('../pages/NotFoundPage'));
const DashboardPage = lazy(() => import('../features/dashboard/pages/DashboardPage'));
const DrivesPage = lazy(() => import('../features/drives/pages/DrivesPage'));
const DriveFormPage = lazy(() => import('../features/drives/pages/DriveFormPage'));
const DriveDetailsPage = lazy(() => import('../features/drives/pages/DriveDetailsPage'));
const RegisterDrivePage = lazy(() => import('../features/registrations/pages/RegisterDrivePage'));
const MyRegistrationsPage = lazy(() => import('../features/registrations/pages/MyRegistrationsPage'));
const RegistrationDetailsPage = lazy(() => import('../features/registrations/pages/RegistrationDetailsPage'));
const ApprovalsPage = lazy(() => import('../features/eligibility/pages/ApprovalsPage'));
const EligibilityPage = lazy(() => import('../features/eligibility/pages/EligibilityPage'));
const AssessmentSubmitPage = lazy(() => import('../features/assessments/pages/AssessmentSubmitPage'));
const DriveAssessmentsPage = lazy(() => import('../features/assessments/pages/DriveAssessmentsPage'));
const VoucherPoolPage = lazy(() => import('../features/vouchers/pages/VoucherPoolPage'));
const MyVouchersPage = lazy(() => import('../features/vouchers/pages/MyVouchersPage'));
const ReportsPage = lazy(() => import('../features/reports/pages/ReportsPage'));

function AppRoutes() {
  return (
    <Suspense fallback={<PageLoader />}>
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/unauthorized" element={<UnauthorizedPage />} />
        <Route path="/404" element={<NotFoundPage />} />

        <Route element={<ProtectedRoute />}>
          <Route element={<MainLayout />}>
            <Route index element={<Navigate to="/dashboard" replace />} />
            <Route path="dashboard" element={<DashboardPage />} />

            <Route element={<RoleGuard roles={routeRoles.candidate} />}>
              <Route path="my-drives" element={<DrivesPage />} />
              <Route path="register-drive" element={<RegisterDrivePage />} />
              <Route path="my-registrations" element={<MyRegistrationsPage />} />
              <Route path="registrations/:registrationId" element={<RegistrationDetailsPage />} />
              <Route path="my-vouchers" element={<MyVouchersPage />} />
              <Route path="assessments/submit/:registrationId" element={<AssessmentSubmitPage />} />
            </Route>

            <Route element={<RoleGuard roles={routeRoles.coordinator} />}>
              <Route path="drives" element={<DrivesPage />} />
              <Route path="drives/new" element={<DriveFormPage />} />
              <Route path="drives/:driveId" element={<DriveDetailsPage />} />
              <Route path="drives/:driveId/registrations" element={<MyRegistrationsPage />} />
              <Route path="drives/:driveId/vouchers" element={<VoucherPoolPage />} />
              <Route path="drives/:driveId/reports" element={<ReportsPage />} />
              <Route path="vouchers/pool" element={<VoucherPoolPage />} />
              <Route path="assessments/drive/:driveId" element={<DriveAssessmentsPage />} />
              <Route path="reports" element={<ReportsPage />} />
            </Route>

            <Route element={<RoleGuard roles={routeRoles.approver} />}>
              <Route path="approvals" element={<ApprovalsPage />} />
              <Route path="eligibility/:registrationId" element={<EligibilityPage />} />
            </Route>
          </Route>
        </Route>

        <Route path="*" element={<Navigate to="/404" replace />} />
      </Routes>
    </Suspense>
  );
}

export default AppRoutes;
