export interface Campaign {
  id: string;
  name: string;
  keywords: string[];
  bidAmount: number;
  campaignFund: number;
  status: string;
  town: string;
  radiusInKm: number;
  userId: string;
}
