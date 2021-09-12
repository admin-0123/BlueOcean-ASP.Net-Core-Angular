import {
    Directive,
    ElementRef,
    EventEmitter,
    HostListener,
    Output
} from '@angular/core';

@Directive({
    selector: '[appClickOutside]'
})
export class ClickOutsideDirective {
    @Output() clickOutside: EventEmitter<any> = new EventEmitter();

    constructor(
        private elementRef: ElementRef
    ) { }

    @HostListener('document:click', ['$event.target'])
    onClickOutside(targetElement: any) {
        if (!this.elementRef.nativeElement.contains(targetElement)) {
            this.clickOutside.emit(false);
        }
    }

    @HostListener('click', ['$event.target'])
    onClickInside(targetElement: any) {
        this.clickOutside.emit(true);
    }
}
