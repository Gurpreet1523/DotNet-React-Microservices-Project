import { Routes, Route } from 'react-router-dom';
import Home from '../pages/Home';
import Projects from '../pages/Projects';
import ProjectDetail from '../pages/ProjectDetail';
import Skills from '../pages/Skills';
import Contact from '../pages/Contact';
import Login from '../pages/Login';
import AdminDashboard from '../pages/AdminDashboard';
import NotFound from '../pages/NotFound';
import ProtectedRoute from '../components/auth/ProtectedRoute';

export default function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/projects" element={<Projects />} />
      <Route path="/projects/:id" element={<ProjectDetail />} />
      <Route path="/skills" element={<Skills />} />
      <Route path="/contact" element={<Contact />} />
      <Route path="/login" element={<Login />} />
      <Route
        path="/admin"
        element={
          <ProtectedRoute>
            <AdminDashboard />
          </ProtectedRoute>
        }
      />
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
}
