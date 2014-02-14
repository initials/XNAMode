#import "SugarBag.h"

static NSString * ImgSugar = @"smallSugarBag.png";

@implementation SugarBag

+ (id) sugarBagWithOrigin:(CGPoint)Origin
{
	return [[[self alloc] initWithOrigin:Origin] autorelease];
}

- (id) initWithOrigin:(CGPoint)Origin
{
	if ((self = [super initWithX:Origin.x y:Origin.y graphic:nil])) {
		
		[self loadGraphicWithParam1:ImgSugar param2:YES param3:NO param4:22 param5:25];
        //self.acceleration = CGPointMake(0, 800);
        self.width = 22;
        self.height = 25;
        self.drag = CGPointMake(1500, 0);
        [self addAnimationWithParam1:@"blink" param2:[NSMutableArray intArrayWithSize:2 ints:0,1] param3:3];
        [self play:@"blink"];
        
	}
	
	return self;	
}

- (void) resetPosition
{
    int levelW = FlxG.levelWidth - self.width;
    self.x = levelW * [FlxU random] ;
    self.y = FlxG.levelHeight * [FlxU random] - (self.height*2);
    
}


- (void) dealloc
{
    
	[super dealloc];
}

- (void) hitBottomWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
    
    [super hitBottomWithParam1:Contact param2:Velocity];
    self.y = roundf(self.y);
    
}



- (void) update
{   
	
	[super update];
	
}


@end
