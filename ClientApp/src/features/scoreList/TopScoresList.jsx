import React, { useCallback, useEffect, useState } from 'react';
import BootstrapTable from 'react-bootstrap-table-next';
import 'react-bootstrap-table-next/dist/react-bootstrap-table2.min.css';

import LoaderScreen from '../../components/loader/LoaderScreen';
import PageHeader from '../../components/pageHeader/PageHeader';

import { requestTopScores } from './scoresApi';
import getScoresColumns, { defaultSorted } from './ScoresColumns';

const TopScoresList = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [scoresList, setScoresList] = useState([]);
    const [countOfTopScores, setCountOfTopScores] = useState(3);

    useEffect(() => {
        (async () => fetchItemList())();
    }, []);

    const fetchItemList = async () => {
        setIsLoading(true);
        try {
            const response = await requestTopScores(countOfTopScores);
            Boolean(response) ? setScoresList(response) : setScoresList([]);
        } catch (error) {
            setScoresList([]);
        }
        setIsLoading(false);
    };

    const columns = useCallback(getScoresColumns(), []);

    return (
        <div>
            <LoaderScreen dim isLoading={isLoading} />
            <PageHeader title={`TOP ${countOfTopScores} SCORES`} />
            <BootstrapTable
                bootstrap4
                keyField='id'
                data={scoresList}
                columns={columns}
                defaultSorted={defaultSorted}
                bordered={false}
                hover
                striped
            />
        </div>
    );
};

export default TopScoresList;
