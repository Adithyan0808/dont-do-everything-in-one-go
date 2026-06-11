import { lazy, Suspense, useMemo, useState } from 'react';
import { Award, ClipboardList, TicketPercent, UsersRound } from 'lucide-react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../hooks/useAuth';
import DashboardSkeleton from '../components/DashboardSkeleton';
import KpiGrid from '../components/kpi/KpiGrid';
import KpiCard from '../components/kpi/KpiCard';
import DashboardHeader from '../sections/DashboardHeader';
import QuickMetricsStrip from '../sections/QuickMetricsStrip';
import DrivePerformanceTable from '../widgets/DrivePerformanceTable';
import DashboardAlertsWidget from '../widgets/DashboardAlertsWidget';
import PendingApprovalsWidget from '../widgets/PendingApprovalsWidget';
import RecentActivityWidget from '../widgets/RecentActivityWidget';
import SlaMonitoringWidget from '../widgets/SlaMonitoringWidget';
import WidgetSkeleton from '../components/WidgetSkeleton';
import { dashboardMockData } from '../services/dashboardMockData';
import { formatNumber, formatPercent } from '../utils/dashboardFormatters';
import {
  useCertificationAnalytics,
  useDashboardAlerts,
  useDrivePerformance,
  useKpiMetrics,
  usePendingApprovals,
  useRecentActivity,
  useRefreshDashboard,
  useSlaMetrics,
} from '../hooks/useDashboardQueries';

const CertificationAnalyticsWidget = lazy(() => import('../widgets/CertificationAnalyticsWidget'));

const percentChange = (current, previous) => {
  if (!previous) return current ? 100 : 0;
  return Math.round(((current - previous) / previous) * 100);
};

function DashboardPage() {
  const [dateRange, setDateRange] = useState('30d');
  const navigate = useNavigate();
  const { hasRole } = useAuth();
  const refreshDashboard = useRefreshDashboard();

  const kpiQuery = useKpiMetrics();
  const drivePerformanceQuery = useDrivePerformance();
  const recentActivityQuery = useRecentActivity();
  const approvalsQuery = usePendingApprovals();
  const slaQuery = useSlaMetrics();
  const alertsQuery = useDashboardAlerts();
  const analyticsQuery = useCertificationAnalytics();

  const canViewOperations = hasRole(['Admin', 'Coordinator']);
  const canViewApprovals = hasRole(['Admin', 'Approver', 'Manager']);
  const isApproverOnly = canViewApprovals && !canViewOperations;

  const kpis = kpiQuery.data || dashboardMockData.overview;
  const quickMetrics = kpis.quickMetrics || dashboardMockData.quickMetrics;

  const passRate = useMemo(() => {
    const passed = kpis.certifiedCount || 0;
    const failed = kpis.failedCount || 0;
    return passed + failed === 0 ? 0 : Math.round((passed / (passed + failed)) * 100);
  }, [kpis.certifiedCount, kpis.failedCount]);

  const voucherUtilization = useMemo(() => {
    const available = kpis.availableVouchers || 0;
    const assigned = kpis.assignedVouchers || 0;
    const redeemed = kpis.redeemedVouchers || 0;
    const total = available + assigned + redeemed;
    return total === 0 ? 0 : Math.round(((assigned + redeemed) / total) * 100);
  }, [kpis.availableVouchers, kpis.assignedVouchers, kpis.redeemedVouchers]);

  const primaryKpis = [
    {
      title: 'Total Active Drives',
      value: formatNumber(kpis.activeDrives || 0),
      previous: formatNumber(kpis.previousActiveDrives || 0),
      trend: percentChange(kpis.activeDrives || 0, kpis.previousActiveDrives || 0),
      icon: ClipboardList,
      tone: 'primary',
      tooltip: 'Currently active certification drives',
      onClick: () => navigate('/drives'),
    },
    {
      title: 'Registered Candidates',
      value: formatNumber(kpis.totalRegistrations || 0),
      previous: formatNumber(kpis.previousRegistrations || 0),
      trend: percentChange(kpis.totalRegistrations || 0, kpis.previousRegistrations || 0),
      icon: UsersRound,
      tone: 'success',
      tooltip: 'Candidates participating across active drives',
      onClick: () => navigate('/drives'),
    },
    {
      title: 'Certification Pass Rate',
      value: formatPercent(passRate),
      previous: `${formatNumber(kpis.certifiedCount || 0)} passed`,
      trend: 6,
      icon: Award,
      tone: 'warning',
      tooltip: 'Overall pass rate from submitted assessments',
      onClick: () => navigate('/reports'),
    },
    {
      title: 'Voucher Utilization',
      value: formatPercent(voucherUtilization),
      previous: `${formatNumber(kpis.availableVouchers || 0)} available`,
      trend: 4,
      icon: TicketPercent,
      tone: 'primary',
      tooltip: 'Assigned and redeemed vouchers against total pool',
      onClick: () => navigate('/vouchers/pool'),
    },
  ];

  if (kpiQuery.isLoading && !kpiQuery.data) {
    return <DashboardSkeleton />;
  }

  return (
    <div className="space-y-5">
      <DashboardHeader dateRange={dateRange} onDateRangeChange={setDateRange} onRefresh={refreshDashboard} />

      {!isApproverOnly && (
        <>
          <KpiGrid>
            {primaryKpis.map((kpi) => (
              <KpiCard key={kpi.title} loading={kpiQuery.isLoading} {...kpi} />
            ))}
          </KpiGrid>
          <QuickMetricsStrip metrics={quickMetrics} />
        </>
      )}

      {canViewOperations && <DrivePerformanceTable query={drivePerformanceQuery} />}

      <div className="grid gap-5 xl:grid-cols-3">
        <div className="space-y-5 xl:col-span-2">
          {!isApproverOnly && <RecentActivityWidget query={recentActivityQuery} />}
          {canViewApprovals && <PendingApprovalsWidget query={approvalsQuery} />}
          {canViewOperations && (
            <Suspense fallback={<WidgetSkeleton title="Certification Analytics" />}>
              <CertificationAnalyticsWidget query={analyticsQuery} />
            </Suspense>
          )}
        </div>

        <div className="space-y-5">
          <SlaMonitoringWidget query={slaQuery} />
          {!isApproverOnly && <DashboardAlertsWidget query={alertsQuery} />}
        </div>
      </div>
    </div>
  );
}

export default DashboardPage;
