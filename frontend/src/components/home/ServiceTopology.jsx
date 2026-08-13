import { useEffect, useState } from 'react';
import healthService, { SERVICE_NODES } from '../../api/healthService';

export default function ServiceTopology() {
  const [nodes, setNodes] = useState(SERVICE_NODES.map((n) => ({ ...n, status: 'checking' })));

  useEffect(() => {
    let cancelled = false;
    healthService.pingAll().then((results) => {
      if (!cancelled) setNodes(results);
    });
    const interval = setInterval(async () => {
      const results = await healthService.pingAll();
      if (!cancelled) setNodes(results);
    }, 30000);
    return () => {
      cancelled = true;
      clearInterval(interval);
    };
  }, []);

  return (
    <div className="topology" aria-label="Live backend service status">
      <div className="topology-track">
        {nodes.map((node, i) => (
          <div className="topology-node" key={node.key}>
            <span
              className={`topology-dot topology-dot--${node.status}`}
              title={`${node.label}: ${node.status}`}
            />
            <span className="topology-label">
              {node.label}
              <span className="topology-port">:{node.port}</span>
            </span>
            {i < nodes.length - 1 && <span className="topology-wire" />}
          </div>
        ))}
      </div>
    </div>
  );
}
