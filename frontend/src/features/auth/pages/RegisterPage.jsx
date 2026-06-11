import AuthCard from '../components/AuthCard';
import AuthLayout from '../components/AuthLayout';
import RegisterForm from '../components/RegisterForm';

export default function RegisterPage() {
  return (
    <AuthLayout title="Create user account" subtitle="Register a Maverick Certification Hub user.">
      <AuthCard>
        <RegisterForm />
      </AuthCard>
    </AuthLayout>
  );
}
