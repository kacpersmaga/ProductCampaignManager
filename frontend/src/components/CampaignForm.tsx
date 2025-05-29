"use client";

import { useEffect, useState } from "react";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Button } from "@/components/ui/button";
import { Campaign } from "@/lib/types";
import {
  Select,
  SelectTrigger,
  SelectValue,
  SelectContent,
  SelectItem,
} from "@/components/ui/select";

const towns = ["Kraków", "Warszawa", "Wrocław", "Gdańsk", "Poznań"];
const keywordSuggestions = ["sport", "tech", "beauty", "gaming", "fashion"];

export default function CampaignForm({
  userId,
  apiUrl,
  onSaved,
  editingCampaign,
  clearEdit,
}: {
  userId: string;
  apiUrl: string;
  onSaved: () => void;
  editingCampaign: Campaign | null;
  clearEdit: () => void;
}) {

  const [form, setForm] = useState({
    name: "",
    keywords: "",
    bidAmount: 1,
    campaignFund: 100,
    status: "On",
    town: towns[0],
    radiusInKm: 10,
  });

  const [errors, setErrors] = useState<{ [key: string]: string }>({});
  const [keywordInput, setKeywordInput] = useState("");
  const [showSuggestions, setShowSuggestions] = useState(false);

  useEffect(() => {
    if (editingCampaign) {
      setForm({
        ...editingCampaign,
        keywords: editingCampaign.keywords.join(", "),
        status: Number(editingCampaign.status) === 1 ? "On" : "Off",
      });
    }
  }, [editingCampaign]);

  const validateForm = () => {
    const newErrors: { [key: string]: string } = {};
    
    if (!form.name.trim()) {
      newErrors.name = "Campaign name is required";
    }
    
    if (!form.keywords.trim()) {
      newErrors.keywords = "At least one keyword is required";
    }
    
    if (form.bidAmount < 0.01) {
      newErrors.bidAmount = "Bid amount must be at least 0.01";
    }
    
    if (form.campaignFund <= 0) {
      newErrors.campaignFund = "Campaign fund must be greater than 0";
    }
    
    if (form.radiusInKm <= 0) {
      newErrors.radiusInKm = "Radius must be greater than 0";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async () => {
    if (!validateForm()) {
      return;
    }

    try {
      const campaignData = {
        ...form,
        keywords: form.keywords.split(",").map(k => k.trim()).filter(Boolean),
        userId,
      };

      if (editingCampaign) {
        await fetch(`${apiUrl}/campaigns/${editingCampaign.id}`, {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(campaignData),
        });
        clearEdit();
      } else {
        await fetch(`${apiUrl}/campaigns`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(campaignData),
        });
      }

      resetForm();
      onSaved();
    } catch (error) {
      console.error("Error saving campaign:", error);
    }
  };

  const resetForm = () => {
    setForm({
      name: "",
      keywords: "",
      bidAmount: 1,
      campaignFund: 100,
      status: "On",
      town: towns[0],
      radiusInKm: 10,
    });
    setKeywordInput("");
    setErrors({});
  };

  const addKeyword = (keyword: string) => {
    const currentKeywords = form.keywords
      .split(",")
      .map(k => k.trim())
      .filter(Boolean);
    
    if (!currentKeywords.includes(keyword) && keyword.trim()) {
      const newKeywords = currentKeywords.length > 0 
        ? `${form.keywords}, ${keyword}` 
        : keyword;
      setForm({ ...form, keywords: newKeywords });
    }
    
    setKeywordInput("");
    setShowSuggestions(false);
  };

  const removeKeyword = (index: number) => {
    const keywords = form.keywords
      .split(",")
      .map(k => k.trim())
      .filter(Boolean);
    
    keywords.splice(index, 1);
    setForm({ ...form, keywords: keywords.join(", ") });
  };

  const getFilteredSuggestions = () => {
    const input = keywordInput.trim().toLowerCase();
    if (!input) return [];
    
    const existingKeywords = form.keywords
      .split(",")
      .map(k => k.trim().toLowerCase());
    
    return keywordSuggestions.filter(
      keyword => 
        keyword.toLowerCase().includes(input) && 
        !existingKeywords.includes(keyword.toLowerCase())
    );
  };

  const filteredSuggestions = getFilteredSuggestions();

  return (
    <div className="space-y-4 border p-6 rounded-lg shadow-md mb-6">
      <h2 className="text-lg font-semibold">
        {editingCampaign ? "Edit Campaign" : "Create New Campaign"}
      </h2>

      {/* Campaign Name */}
      <div>
        <Label htmlFor="name">Campaign Name</Label>
        <Input 
          id="name" 
          value={form.name} 
          onChange={e => setForm({ ...form, name: e.target.value })}
          placeholder="Enter campaign name"
        />
        {errors.name && <p className="text-red-500 text-sm mt-1">{errors.name}</p>}
      </div>

      {/* Keywords */}
      <div>
        <Label>Keywords</Label>
        
        {/* Display current keywords */}
        <div className="flex flex-wrap gap-2 mb-2">
          {form.keywords.split(",").map((keyword, index) =>
            keyword.trim() ? (
              <span 
                key={index} 
                className="bg-blue-100 text-blue-800 px-2 py-1 rounded-full text-xs flex items-center gap-1"
              >
                {keyword.trim()}
                <button 
                  type="button" 
                  onClick={() => removeKeyword(index)}
                  className="hover:bg-blue-200 rounded-full w-4 h-4 flex items-center justify-center"
                >
                  ×
                </button>
              </span>
            ) : null
          )}
        </div>

        {/* Keyword input */}
        <div className="relative">
          <Input
            placeholder="Type a keyword and press Enter"
            value={keywordInput}
            onChange={e => setKeywordInput(e.target.value)}
            onKeyDown={e => {
              if (e.key === "Enter") {
                e.preventDefault();
                addKeyword(keywordInput);
              }
            }}
            onFocus={() => setShowSuggestions(true)}
            onBlur={() => setTimeout(() => setShowSuggestions(false), 200)}
          />
          
          {/* Keyword suggestions */}
          {showSuggestions && filteredSuggestions.length > 0 && (
            <div className="absolute top-full left-0 right-0 bg-white border rounded-md shadow-lg z-10 max-h-40 overflow-y-auto">
              {filteredSuggestions.map((keyword, index) => (
                <div
                  key={index}
                  className="px-3 py-2 hover:bg-gray-100 cursor-pointer"
                  onClick={() => addKeyword(keyword)}
                >
                  {keyword}
                </div>
              ))}
            </div>
          )}
        </div>
        
        {errors.keywords && <p className="text-red-500 text-sm mt-1">{errors.keywords}</p>}
      </div>

      {/* Bid Amount */}
      <div>
        <Label htmlFor="bid">Bid Amount (Emeralds)</Label>
        <Input 
          id="bid" 
          type="number" 
          min="0.01"
          step="0.01"
          value={form.bidAmount} 
          onChange={e => setForm({ ...form, bidAmount: parseFloat(e.target.value) || 0 })} 
        />
        {errors.bidAmount && <p className="text-red-500 text-sm mt-1">{errors.bidAmount}</p>}
      </div>

      {/* Campaign Fund */}
      <div>
        <Label htmlFor="fund">Campaign Fund (Emeralds)</Label>
        <Input 
          id="fund" 
          type="number" 
          min="1"
          value={form.campaignFund} 
          onChange={e => setForm({ ...form, campaignFund: parseInt(e.target.value) || 0 })} 
        />
        {errors.campaignFund && <p className="text-red-500 text-sm mt-1">{errors.campaignFund}</p>}
      </div>

      {/* Campaign Status */}
      <div>
        <Label>Campaign Status</Label>
        <Select 
          value={form.status} 
          onValueChange={value => setForm({ ...form, status: value })}
        >
          <SelectTrigger className="w-full">
            <SelectValue />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value="On">Active</SelectItem>
            <SelectItem value="Off">Inactive</SelectItem>
          </SelectContent>
        </Select>
      </div>

      {/* Town Selection */}
      <div>
        <Label>Target City</Label>
        <Select 
          value={form.town} 
          onValueChange={value => setForm({ ...form, town: value })}
        >
          <SelectTrigger className="w-full">
            <SelectValue />
          </SelectTrigger>
          <SelectContent>
            {towns.map((town) => (
              <SelectItem key={town} value={town}>
                {town}
              </SelectItem>
            ))}
          </SelectContent>
        </Select>
      </div>

      {/* Radius */}
      <div>
        <Label htmlFor="radius">Radius (kilometers)</Label>
        <Input 
          id="radius" 
          type="number" 
          min="1"
          value={form.radiusInKm} 
          onChange={e => setForm({ ...form, radiusInKm: parseInt(e.target.value) || 0 })} 
        />
        {errors.radiusInKm && <p className="text-red-500 text-sm mt-1">{errors.radiusInKm}</p>}
      </div>

      {/* Submit Button */}
      <div className="flex gap-2">
        <Button onClick={handleSubmit} className="flex-1">
          {editingCampaign ? "Update Campaign" : "Create Campaign"}
        </Button>
        
        {editingCampaign && (
          <Button variant="outline" onClick={() => { clearEdit(); resetForm(); }}>
            Cancel
          </Button>
        )}
      </div>
    </div>
  );
}