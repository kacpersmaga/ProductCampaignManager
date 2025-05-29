"use client";

import { Campaign } from "@/lib/types";
import { Button } from "@/components/ui/button";

interface Props {
  campaigns: Campaign[];
  onEdit: (campaign: Campaign) => void;
  onDelete: (id: string) => void;
}

export default function CampaignList({ campaigns, onEdit, onDelete }: Props) {
  return (
    <div className="space-y-4 mt-8">
      {campaigns.map((c) => (
        <div
          key={c.id}
          className="border p-4 rounded-lg shadow-sm flex justify-between items-center"
        >
          <div>
            <h2 className="text-xl font-semibold">{c.name}</h2>
            <p className="text-sm text-gray-600">
              Fund: {c.campaignFund} | Bid: {c.bidAmount} | Status: {c.status}
            </p>
          </div>
          <div className="flex gap-2">
            <Button variant="outline" onClick={() => onEdit(c)}>
              Edit
            </Button>
            <Button variant="destructive" onClick={() => onDelete(c.id)}>
              Delete
            </Button>
          </div>
        </div>
      ))}
    </div>
  );
}
