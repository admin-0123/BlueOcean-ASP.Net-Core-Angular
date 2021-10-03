import {
    animate,
    group,
    query,
    style,
    transition,
    trigger
} from '@angular/animations';

const openUpPLP = [
    query(':leave > div', [
        style({
            opacity: 1
        })
    ], { optional: true }),
    query(':enter', [
        style({
            position: 'absolute',
            overflow: 'hidden',
            top: '{{ top }}',
            left: '{{ left }}',
            width: '225px',
            maxHeight: '300px',
            zIndex: 10
        }),
        query('#splide img', [
            style({
                width: '225px',
                height: '300px'
            })
        ])
    ]),
    group([
        query(':leave > div', [
            animate('550ms ease',
                style({
                    opacity: 0
                })
            )
        ], { optional: true }),
        query(':enter', [
            animate('400ms ease',
                style({
                    maxHeight: '1000px',
                    width: '100%',
                    top: 0,
                    left: 0,
                    padding: '0'
                })
            )
        ]),
        query(':enter #splide img', [
            animate('400ms ease',
                style({
                    width: '*',
                    height: '*'
                })
            )
        ])
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
        })
    ]),
    query(':leave #splide', [
        style({
            position: 'absolute',
            left: 0,
            top: 0
        })
    ]),
    query(':leave #splide .is-active img', [
        style({
            width: '450px',
            height: '600px'
        })
    ]),
    group([
        query(':leave', [
            animate('400ms ease',
                style({
                    width: '225px',
                    maxHeight: '300px',
                    top: '{{ top }}',
                    left: '{{ left }}'
                })
            )
        ]),
        query(':leave #splide .is-active img', [
            animate('400ms ease',
                style({
                    width: '225px',
                    height: '300px'
                })
            )
        ])
    ])
];

export const slider =
    trigger('routeAnimations', [
        transition('* => PDP', openUpPLP, { params: { top: '0px', left: '200px' } }),
        transition('PDP => PLP', closeUp, { params: { top: '0px', left: '200px' } })
    ]);
