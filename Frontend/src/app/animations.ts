import {
    animate,
    query,
    style,
    transition,
    trigger
} from '@angular/animations';

const openUp = [
    query(':leave', [
        style({
            zIndex: -1
        })
    ]),
    query(':enter', [
        style({
            position: 'absolute',
            overflow: 'hidden',
            top: '{{ top }}',
            left: '{{ left }}',
            width: '225px',
            maxHeight: '300px',
            zIndex: 10,
            backgroundColor: 'white'
        }),
        animate('50ms ease',
            style({
                top: '{{ top2 }}',
                left: '{{ left2 }}',
                padding: '{{ padding }}',
                width: '285px',
                maxHeight: '360px'
            })
        ),
        animate('400ms ease',
            style({
                maxHeight: '1000px',
                width: '100%',
                top: 0,
                left: 0,
                padding: '0'
            })
        )
    ])
];

const closeUp = [
    query(':enter', [
        style({
            zIndex: -1
        })
    ]),
    query(':leave', [
        style({
            position: 'absolute',
            overflow: 'hidden',
            width: '100%',
            maxHeight: '1000px',
            zIndex: 10,
            left: 0,
            top: 0
        }),
        animate('400ms ease',
            style({
                width: '225px',
                maxHeight: '300px',
                top: '{{ top }}',
                left: '{{ left }}'
            })
        ),
        animate('50ms',
            style({
                width: '225px',
                maxHeight: '300px',
                top: '{{ top }}',
                left: '{{ left }}'
            })
        )
    ])
];

export const slider =
    trigger('routeAnimations', [
        transition('* => PDP', openUp, { params: { top: '0px', left: '200px' } }),
        transition('PDP => *', closeUp, { params: { top: '0px', left: '200px' } })
    ]);
