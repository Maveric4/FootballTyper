import React from 'react'
import './GroupStageMatches.scss';
import Matchrow  from '../../Matchrow/Matchrow'
import { GroupMatch } from '../GroupStage';

import Spinner from 'react-bootstrap/Spinner'
interface GroupStageMatchesProps {
  groupMatches: GroupMatch[] | null,
  chosenCountries: {homeCountry: string, awayCountry: string},
  setChosenCountries: React.Dispatch<React.SetStateAction<{
    homeCountry: string;
    awayCountry: string;
}>>,

}

const GroupStageMatches = ({groupMatches, chosenCountries, setChosenCountries} : GroupStageMatchesProps) => {
  return (
    <div className='group-stage-matches-content' style={{width: '97%', display: 'grid', gridTemplateColumns: 'repeat(2, 1fr)', gridTemplateRows: 'repeat(3, 1fr)', gap: '5px'}}>
      { groupMatches ? groupMatches.map((item: GroupMatch, index) => (
        <div>
        <Matchrow groupMatch={item} key={index} chosenCountries={chosenCountries} setChosenCountries={setChosenCountries}/>
        </div>
      )) : <Spinner animation='border' />

      }
    </div>
  )
}

export default GroupStageMatches