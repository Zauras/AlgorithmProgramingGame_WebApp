import React from 'react';

export const defaultSorted = [{ dataField: 'id', order: 'asc' }];

const getScoresColumns = () => [
    {
        dataField: 'placeIndex',
        text: 'PLACE',
    },
    {
        dataField: 'name',
        text: 'NAME',
    },
    {
        dataField: 'successCount',
        text: 'SUCCESS SOLUTIONS',
    },
    {
        dataField: 'successRate',
        text: 'SUCCESS RATE',
    },
    {
        dataField: 'tasks',
        text: 'TASKS',
        //formatter: (cell, row, rowIndex, formatExtraData) => (cell === 1 ? `${cell} (You)` : cell),
    },
];

export default getScoresColumns;
