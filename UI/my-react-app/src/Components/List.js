import React, { useState, useEffect } from 'react';
import { fetchData } from '../Api';
import BootstrapTable from 'react-bootstrap-table-next';
import 'bootstrap/dist/css/bootstrap.min.css';

const columns = [
  {
    dataField: 'type',
    text: 'Type',
  },
  {
    dataField: 'date',
    text: 'Date',
  },
  {
    dataField: 'product',
    text: 'Product',
  },
  {
    dataField: 'amount',
    text: 'Amount',
  },
  {
    dataField: 'seller',
    text: 'Seller',
  },
];

const List = () => {
  const [data, setData] = useState([]);

  useEffect(() => {
    fetchData({ page: 1, pageSize: 10, sortBy: 'Seller', sortDirection: 'asc' }).then((data) => {
      setData(data);
    });
  }, []);

  return (
    <div>
      <h1>Data List</h1>
      <BootstrapTable keyField="id" data={data} columns={columns} />
    </div>
  );
};

export default List;