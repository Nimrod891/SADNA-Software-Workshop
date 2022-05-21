const {fomat} = require('date-fns');
const {v4:uuid} = require('uuid');

const fs=require('fs');
const fsPromises=require('fs').promises;
const path=require('path');

const logEvents=async (message) => {
    const dateTime=`${format(new Date(), 'ddMMyyyy\tHH:mm:ss')}`;
    const logItem=`${dateTIme}\t${uuid()}\t${message}`;

    console.log(logItem)
    try{
        if(!fs.existsSync(path.join(__dirname, 'logs'))){
            await fsPromises.mkdir(path.join(__dirname, 'logs'));
        }
        await fsPromises.appendFile(path.join(__dirname, 'logs', 'eventLog.txt'),logItem);
        // appendFile will create one if it doesnt exist.
    } catch(err){
        console.error(err);
        //testing
    }
}

module.exports=logEvents;