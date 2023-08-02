import axios from 'axios';

const API_URL = 'http://localhost:8001/transactions/list'; // Replace with your API endpoint

export const fetchData = async (params) => {
  try {
    const response = await axios.get(API_URL, { params });
    return response.data;
  } catch (error) {
    console.error('Error fetching data:', error);
    return [];
  }
};