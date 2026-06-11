import AuthCard from '../components/AuthCard';
import AuthLayout from '../components/AuthLayout';
import LoginForm from '../components/LoginForm';

export default function LoginPage() {
  return (
    <AuthLayout title="Welcome back" subtitle="Sign in to manage certification drives and approvals.">
      <AuthCard>
        <LoginForm />
      </AuthCard>
    </AuthLayout>
  );
}
