import { Link } from 'react-router-dom';

export default function ProjectCard({ project }) {
  return (
    <Link to={`/projects/${project.id}`} className="project-card card">
      <div className="project-card-head">
        <h3>{project.title}</h3>
        <span className="project-card-year">{project.year}</span>
      </div>
      <p className="project-card-desc">{project.shortDescription}</p>
      {project.tags?.length > 0 && (
        <div className="project-card-tags">
          {project.tags.map((tag) => (
            <span className="tag" key={tag}>
              {tag}
            </span>
          ))}
        </div>
      )}
    </Link>
  );
}
