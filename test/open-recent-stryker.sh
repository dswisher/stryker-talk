#!/bin/bash

DIR=StrykerOutput
SUBDIR=$(ls -Art $DIR | tail -n 1)
REPORT=$DIR/$SUBDIR/reports/mutation-report.html

if [ -f $REPORT ]; then
    open $REPORT
else
    echo "$REPORT does not exist!?"
fi

