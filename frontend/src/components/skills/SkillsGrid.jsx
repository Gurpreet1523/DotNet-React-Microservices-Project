import { useFetch } from '../../hooks/useFetch';
import skillsService from '../../api/skillsService';
import Loader from '../common/Loader';
import ErrorMessage from '../common/ErrorMessage';
import SkillBadge from './SkillBadge';

export default function SkillsGrid() {
  const { data: skills, loading, error, refetch } = useFetch(skillsService.getAll, []);

  if (loading) return <Loader label="Loading skills" />;
  if (error) return <ErrorMessage message="Couldn't reach the Skills service." onRetry={refetch} />;
  if (!skills?.length) return <p className="empty-state">No skills listed yet.</p>;

  const grouped = groupByCategory(skills);

  return (
    <div className="skills-groups">
      {Object.entries(grouped).map(([category, items]) => (
        <div key={category} className="skills-group">
          <h3 className="skills-group-title">{category}</h3>
          <div className="skills-group-list">
            {items.map((skill) => (
              <SkillBadge key={skill.id} skill={skill} />
            ))}
          </div>
        </div>
      ))}
    </div>
  );
}

function groupByCategory(skills) {
  return skills.reduce((acc, skill) => {
    const category = skill.category || 'Other';
    acc[category] = acc[category] || [];
    acc[category].push(skill);
    return acc;
  }, {});
}
