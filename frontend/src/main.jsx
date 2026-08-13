import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';

// Global
import './styles/global.css';

// Layout
import './components/layout/Navbar.css';
import './components/layout/Footer.css';

// Common
import './components/common/Loader.css';
import './components/common/ErrorMessage.css';

// Home
import './components/home/Hero.css';
import './components/home/ServiceTopology.css';
import './components/home/ExperienceTimeline.css';

// Projects
import './components/projects/ProjectCard.css';
import './pages/ProjectDetail.css';

// Skills
import './components/skills/SkillsGrid.css';

// Contact / Auth forms
import './components/contact/ContactForm.css';

// Pages
import './pages/AdminDashboard.css';
import './pages/shared.css';

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
