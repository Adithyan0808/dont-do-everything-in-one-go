import { Toaster } from 'react-hot-toast';
import AppRoutes from './routes/AppRoutes';

function App() {
  return (
    <>
      <AppRoutes />
      <Toaster position="top-right" toastOptions={{ duration: 3500 }} />
    </>
  );
}

export default App;
