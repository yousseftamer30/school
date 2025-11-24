import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { Toaster } from 'sonner';
import MainLayout from '@/components/layout/MainLayout';
import Dashboard from '@/pages/Dashboard';
import Login from '@/pages/Login';
import ProtectedRoute from '@/components/layout/ProtectedRoute';
import SchoolList from '@/pages/Schools/SchoolList';

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 1000 * 60 * 5, // 5 minutes
      refetchOnWindowFocus: false,
    },
  },
});

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Routes>
          {/* Public Routes */}
          <Route path="/login" element={<Login />} />

          {/* Protected Routes */}
          <Route element={<ProtectedRoute />}>
            <Route path="/" element={<MainLayout />}>
              <Route index element={<Dashboard />} />
              
              {/* Operations */}
              <Route path="customers" element={<div>Customers Page</div>} />
              <Route path="reservations" element={<div>Reservations Page</div>} />
              <Route path="payments" element={<div>Payments Page</div>} />
              <Route path="sessions" element={<div>Sessions Page</div>} />

              {/* Lookups */}
              <Route path="schools" element={<SchoolList />} />
              <Route path="license-types" element={<div>License Types Page</div>} />
              <Route path="license-groups" element={<div>License Groups Page</div>} />
              <Route path="roles" element={<div>Roles Page</div>} />
              <Route path="employees" element={<div>Employees Page</div>} />
              <Route path="vehicles" element={<div>Vehicles Page</div>} />
              <Route path="traffic-units" element={<div>Traffic Units Page</div>} />
              <Route path="transmission-types" element={<div>Transmission Types Page</div>} />
              <Route path="course-lists" element={<div>Course Lists Page</div>} />
            </Route>
          </Route>

          {/* Catch all redirect */}
          <Route path="*" element={<Navigate to="/" replace />} />
        </Routes>
      </BrowserRouter>
      <Toaster />
    </QueryClientProvider>
  );
}

export default App;
