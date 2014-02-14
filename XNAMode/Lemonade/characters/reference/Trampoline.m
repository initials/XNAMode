#import "Trampoline.h"

@implementation Trampoline

+ (id) trampolineWithOrigin:(CGPoint)Origin
{
	return [[[self alloc] initWithOrigin:Origin] autorelease];
}

- (id) initWithOrigin:(CGPoint)Origin
{
	if ((self = [super initWithX:Origin.x y:Origin.y graphic:nil])) {
		
		[self loadGraphicWithParam1:@"trampoline.png" param2:YES param3:NO param4:24 param5:24];
        
        //[self createGraphicWithParam1:30 param2:30 param3:0xff0000];
        
        [self addAnimationWithParam1:@"stuck" param2:[NSMutableArray intArrayWithSize:1 ints:0] param3:0];
        [self addAnimationWithParam1:@"boing" param2:[NSMutableArray intArrayWithSize:4 ints:1,2,0,0] param3:12 param4:NO];

        self.width=24;
        self.height = 24;
        
        [self play:@"stuck"];
        
        
	}
	
	return self;
}

- (void) dealloc
{
    
	[super dealloc];
}

- (void) hitBottomWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
    //[self play:@"boing"];

    [super hitBottomWithParam1:Contact param2:Velocity];
    
}

- (void) hitTopWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
    //[self play:@"boing"];

    [super hitTopWithParam1:Contact param2:Velocity];
    
}

- (void) update
{
	
	[super update];
	
}


@end
