import { useState, useEffect, useCallback } from 'react';

/**
 * Runs fetchFn on mount (and whenever deps change), tracking loading/error/data.
 * Components never call axios/services directly — this hook + the api/ layer
 * is the only bridge between UI and the network.
 */
export function useFetch(fetchFn, deps = []) {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const run = useCallback(() => {
    let cancelled = false;
    setLoading(true);
    setError(null);

    fetchFn()
      .then((res) => {
        if (!cancelled) setData(res);
      })
      .catch((err) => {
        if (!cancelled) setError(err);
      })
      .finally(() => {
        if (!cancelled) setLoading(false);
      });

    return () => {
      cancelled = true;
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, deps);

  useEffect(() => run(), [run]);

  return { data, loading, error, refetch: run };
}
