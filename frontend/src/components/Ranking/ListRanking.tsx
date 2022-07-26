import React, { useContext, useEffect, useState } from 'react';
import './ListRanking.scss';
import Avatar from '@mui/material/Avatar';
import { User } from '../../App';
import { BsCheck } from "react-icons/bs";
import { BiCheckDouble } from "react-icons/bi"
import { ImCross } from 'react-icons/im';
import Table from 'react-bootstrap/Table';
import { UserContext } from '../../App';
import { FaCrown } from 'react-icons/fa';
import Tooltip from 'react-bootstrap/Tooltip';
import OverlayTrigger from 'react-bootstrap/OverlayTrigger';
import { VscTriangleDown, VscTriangleUp } from 'react-icons/vsc';
import { MdOutlineHorizontalRule } from 'react-icons/md';

import avatar1 from './avatar1.jpg';
import avatar2 from './avatar2.png';
import { RankingFilters } from './Ranking';
import { isMobile } from 'react-device-detect';

export interface ListRankingProps {
    league: string,
    allUsers: User[],
    filter: RankingFilters
}

const ListRanking: React.FC<ListRankingProps> = ({ allUsers, league, filter }) => {
    const userCtx = useContext(UserContext);
    const [update, setUpdate] = useState<null | Date>(null);
    const API_URL = process.env.REACT_APP_IS_IT_PRODUCTION_VERSION === 'true' ? process.env.REACT_APP_API_URL_PROD : process.env.REACT_APP_API_URL_LOCAL;

    useEffect(() => {
        const getUpdate = async () => {
            const lastUpdate = await (await fetch(API_URL + `api/Score/LastUpdate`)).json();
            setUpdate(new Date(lastUpdate));
        }
        getUpdate()
    }, [])
    return (
        <div className='ranking-table'>
            {update ?
                <div className='last-update-score'>
                    LAST UPDATE - {update.toLocaleString()}
                </div>
                : null
            }
            <Table striped>
                <thead className='table-main'>
                    <tr>
                        <th className='rank-row'>Rank</th>
                        <th className='empty-row'></th>
                        <th> User </th>
                        <th> Points </th>
                        <OverlayTrigger
                            overlay={
                                <Tooltip id={'correctscores'}>
                                    Correct Bet Score
                                </Tooltip>
                            }
                            placement='top-start'
                        >
                            <th>
                                CBS
                            </th>
                        </OverlayTrigger>
                        <OverlayTrigger
                            overlay={
                                <Tooltip id={'correctresults'}>
                                    Correct Bet
                                </Tooltip>
                            }
                            placement='top-start'
                        >
                            <th>
                                CB
                            </th>
                        </OverlayTrigger>
                        <OverlayTrigger
                            overlay={
                                <Tooltip id={'wrongbets'}>
                                    Wrong Bets
                                </Tooltip>
                            }
                            placement='top-start'
                        >
                            <th>
                                WB
                            </th>
                        </OverlayTrigger>
                        <OverlayTrigger
                            overlay={
                                <Tooltip id={'last5matches'}>
                                    Last 5 user bets
                                </Tooltip>
                            }
                            placement='top-start'
                        >
                            <th style={isMobile ? { textAlign: 'center', minWidth: '25vw' } : { textAlign: 'center' }}>
                                Form
                            </th>
                        </OverlayTrigger>
                    </tr>
                </thead>
                <tbody className='ranking-row-content'>
                    {allUsers.map(({ totalPoints, username, totalCorrectWinnerBets, totalWrongBets, totalExactScoreBets, lastFiveBets, rankStatusDict, imgLink, fullName, positionDict }, index) => {
                        type ObjectKey = keyof typeof rankStatusDict;
                        const leagueName = league as ObjectKey;
                        const userRankingStatus = rankStatusDict ? rankStatusDict[leagueName] : 0;
                        return (
                            <tr key={index} style={userCtx.userLocalData?.username === username ? { boxShadow: '2px 2px 5px 5px lightblue', alignItems: 'center', height: '100%' } : { alignItems: 'center' }}>
                                <td style={{ margin: 'auto 0 !important', padding: '0px 0px !important', textAlign: 'right' }}>
                                    <div>
                                        {
                                            userRankingStatus > 0 && filter === 'general' ? <VscTriangleUp style={{ color: 'green', height: '1.2vh', margin: 'auto 0 !important' }} /> :
                                                (userRankingStatus < 0 && filter === 'general' ? <VscTriangleDown style={{ color: 'red', margin: '0 0 !important', padding: '0px 0px !important', height: '1.2vh' }} />
                                                    : undefined)
                                        }
                                        <span style={{ fontSize: '3.5vh', fontWeight: '500' }}>{positionDict[leagueName]}</span>
                                    </div>

                                </td>
                                <td> {positionDict[leagueName] === 1 ? <FaCrown style={{ color: 'orange', alignItems: 'center', height: '2.2vh', width: '2.2vh', padding: '0 0 !important' }} /> : null}</td>

                                <td>
                                    <Avatar style={{ height: '4vh', width: '4vh', float: 'left', marginRight: '0.9vw' }} src={imgLink ? imgLink : undefined} alt={username} />
                                    <span style={userCtx.userLocalData?.username === username ? { fontWeight: '700', fontSize: '2.5vh' } : { fontSize: '2.5vh' }}>{fullName}</span>
                                </td>

                                <td style={userCtx.userLocalData?.username === username ? { fontWeight: '700' } : undefined}> {totalPoints}</td>
                                <td style={userCtx.userLocalData?.username === username ? { fontWeight: '700' } : undefined}> {totalExactScoreBets}</td>
                                <td style={userCtx.userLocalData?.username === username ? { fontWeight: '700' } : undefined}> {totalCorrectWinnerBets}</td>
                                <td style={userCtx.userLocalData?.username === username ? { fontWeight: '700' } : undefined}> {totalWrongBets}</td>
                                <td style={{ textAlign: 'center' }}>
                                    {
                                        lastFiveBets !== "" ?
                                            lastFiveBets?.split(',').map(Number).map((userBet, index) => {
                                                switch (userBet) {
                                                    case 0:
                                                        return <ImCross style={isMobile ? { color: 'red', margin: '0 0.5vw', width: '2.25vw', height: '2.25vw' } : { color: 'red', margin: '0 0.5vw', width: '1.25vw', height: '1.25vw' }} key={index} />
                                                    case 1:
                                                        return <BsCheck style={isMobile ? { color: 'lightgreen', width: '4vw', height: '4vw' } : { color: 'lightgreen', width: '2.25vw', height: '2.25vw' }} key={index} />
                                                    case 2:
                                                        return <BiCheckDouble style={isMobile ? { color: 'green', width: '4vw', height: '4vw' } : { color: 'green', width: '2.25vw', height: '2.25vw' }} key={index} />
                                                }
                                            })
                                            : null
                                    }
                                </td>
                            </tr>
                        )
                    })
                    }
                </tbody>
            </Table>
        </div>

    );
}

export default ListRanking;