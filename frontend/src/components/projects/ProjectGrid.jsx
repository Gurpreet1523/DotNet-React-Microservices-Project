import { useFetch } from '../../hooks/useFetch';
import projectsService from '../../api/projectsService';
import Loader from '../common/Loader';
import ErrorMessage from '../common/ErrorMessage';
import ProjectCard from './ProjectCard';

export default function ProjectGrid() {
  const { data: projects, loading, error, refetch } = useFetch(projectsService.getAll, []);

  if (loading) return <Loader label="Loading projects" />;
  if (error) return <ErrorMessage message="Couldn't reach the Projects service." onRetry={refetch} />;
  if (!projects?.length) return <p className="empty-state">No projects published yet.</p>;

  return (
    <div className="project-grid">
      {projects.map((project) => (
        <ProjectCard key={project.id} project={project} />
      ))}
    </div>
  );
}
