import { useParams, Link } from 'react-router-dom';
import { useFetch } from '../hooks/useFetch';
import projectsService from '../api/projectsService';
import Loader from '../components/common/Loader';
import ErrorMessage from '../components/common/ErrorMessage';

export default function ProjectDetail() {
  const { id } = useParams();
  const {
    data: project,
    loading,
    error,
    refetch,
  } = useFetch(() => projectsService.getById(id), [id]);

  return (
    <section className="section">
      <div className="container">
        <Link to="/projects" className="btn">
          ← Back to projects
        </Link>

        {loading && <Loader label="Loading project" />}
        {error && (
          <ErrorMessage message="Couldn't reach the Projects service." onRetry={refetch} />
        )}

        {project && (
          <div className="project-detail">
            <p className="eyebrow">{project.year}</p>
            <h1>{project.title}</h1>
            <p className="project-detail-desc">{project.description}</p>

            {project.tags?.length > 0 && (
              <div className="project-card-tags">
                {project.tags.map((tag) => (
                  <span className="tag" key={tag}>
                    {tag}
                  </span>
                ))}
              </div>
            )}

            <div className="project-detail-links">
              {project.repoUrl && (
                <a className="btn" href={project.repoUrl} target="_blank" rel="noreferrer">
                  Source
                </a>
              )}
              {project.liveUrl && (
                <a className="btn btn-primary" href={project.liveUrl} target="_blank" rel="noreferrer">
                  Live demo
                </a>
              )}
            </div>
          </div>
        )}
      </div>
    </section>
  );
}
