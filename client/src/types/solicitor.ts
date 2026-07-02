export interface Solicitor {
  name: string;
  location: string;
  phoneNumber: string | null;
  address: string | null;
  reviewCount: number | null;
  description: string | null;
  websiteUrl: string | null;
}

export interface SearchResponse {
  solicitors: Solicitor[];
}
