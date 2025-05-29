"use client";

import { useEffect, useState } from "react";
import axios from "axios";
import CampaignForm from "../components/CampaignForm";
import CampaignList from "../components/CampaignList";
import { Campaign } from "@/lib/types";

const USER_ID = "00000000-0000-0000-0000-000000000001";
const API_URL = "https://localhost:7263/api";

export default function CampaignsPage() {
  const [campaigns, setCampaigns] = useState<Campaign[]>([]);
  const [balance, setBalance] = useState<number>(0);
  const [editingCampaign, setEditingCampaign] = useState<Campaign | null>(null);

  const refresh = async () => {
    const [camps, bal] = await Promise.all([
      axios.get(`${API_URL}/campaigns?userId=${USER_ID}`),
      axios.get(`${API_URL}/users/${USER_ID}/balance`),
    ]);
    setCampaigns(camps.data);
    setBalance(bal.data.balance);
  };

  useEffect(() => {
    refresh();
  }, []);

  return (
    <div className="p-6 max-w-3xl mx-auto">
      <h1 className="text-2xl font-bold mb-4">Campaign Manager</h1>
      <p className="mb-6">
        User Balance: <strong>{balance.toFixed(2)} Emeralds</strong>
      </p>

      <CampaignForm
        userId={USER_ID}
        apiUrl={API_URL}
        onSaved={refresh}
        editingCampaign={editingCampaign}
        clearEdit={() => setEditingCampaign(null)}
      />

      <CampaignList
        campaigns={campaigns}
        onDelete={async (id: string) => {
          await axios.delete(`${API_URL}/campaigns/${id}`);
          refresh();
        }}
        onEdit={(campaign: Campaign) => setEditingCampaign(campaign)}
      />
    </div>
  );
}
