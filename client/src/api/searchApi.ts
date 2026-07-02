import type { Solicitor } from '../types/solicitor';

const API_BASE_URL = 'http://localhost:5295';

export async function fetchLocations(): Promise<string[]> {
  const response = await fetch(`${API_BASE_URL}/api/search/locations`);

  if (!response.ok) {
    throw new Error(`Failed to fetch locations: ${response.status}`);
  }

  return response.json();
}

export async function searchSolicitors(locations: string[]): Promise<Solicitor[]> {
  const params = new URLSearchParams();
  locations.forEach(loc => params.append('locations', loc));

  const response = await fetch(`${API_BASE_URL}/api/search/conveyancy?${params.toString()}`);

  if (!response.ok) {
    const body = await response.json().catch(() => null);
    throw new Error(body?.error ?? `Search failed: ${response.status}`);
  }

  return response.json();
}
