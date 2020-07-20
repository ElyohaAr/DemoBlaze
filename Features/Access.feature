Feature: Access
	In order to buy things
	As an unregisterd user
	I want to register and log in 

Scenario: Register new user
	Given I am on the homepage
	And I click on Sign Up button
	When I fill in required data for RandomUser
	Then I get registered

Scenario: Register known user
	Given I am on the homepage
	And I click on Sign Up button
	When I fill in required data for User01
	Then I get informed that the username is taken

Scenario: Log In
	Given I am on the homepage
	When I click on the login button
	And I enter my credentials
	Then I get logged in

Scenario: Check that Image Slider change the content for Next
	Given I am on the homepage
	When I click on the “Next” buttons from Image Slider
	Then I see a different product

Scenario: Check that Image Slider change the content for Previous
	Given I am on the homepage
	When I click on the “Previous” buttons from Image Slider
	Then I see a different product

Scenario: Buy random phones using given budget
	Given I am logged in
	And I have a budget of 1500$
	When I filter by Phones
	Then I can add to cart 2 random phones that don't exceed my budget

Scenario Outline: Get mean value product cost
	Given I am on the homepage
	When I filter by <Product>
	Then I can see in the test output the mean value of each product

	Examples:
		| Product  |
		| Phones   |
		| Laptops  |
		| Monitors |

Scenario: Check Home Page
	Given I am on the homepage
	And I click on Home page
	Then the Home page is displayed

Scenario: Check Cart page
	Given I am on the homepage
	And I click on Cart page
	Then the Cart page is displayed

Scenario: Select first available item
	Given I am on the homepage
	And I click on the first product available
	Then I can see it's price


Scenario:  Empty Cart
	Given I am logged in
	And I click on Cart page
	And I have items in Cart
	And I remove them
	Then My cart is empty

#Access the home page, select the first available product, add it to your cart, navigate to the shopping cart page, then the selected product with the correct price should be displayed
@EmptyCart
Scenario: Check product price
	Given I am logged in
	And I click on the first product available
	And I add the product to my cart
	And I click on Cart page
	Then The selected product should be in cart

#Create a parameterized test (using scenario outline) that can access all the pages from the header and check that the correct page/popup is displayed
Scenario: Check each button from header
	Given I am on the homepage
	And I click on <pageType> page
	Then the <wantedPage> page is displayed

	Examples:
		| pageType | wantedPage |
		| Contact  | Contact    |
		| Cart     | Cart       |
		| About us | About us   |
		| Home     | Home       |
		| Log in   | Log in     |
		| Sign up  | Sign up    |

Scenario: Check each button from header when logged in
	Given I am logged in
	And I click on <pageType> page
	Then the <wantedPage> page is displayed

	Examples:
		| pageType | wantedPage |
		| Contact  | Contact    |
		| Cart     | Cart       |
		| Log out  | Log out    |
		| About us | About us   |
		| Home     | Home       |

#Scenario: Buy a Dell laptop model from 2017
@EmptyCart
Scenario:Buy a Dell laptop model from 2017
	Given I added in my cart a laptop from 2017
	And I click on Cart page
	And I click on Place order button
	When I fill the required data to purchase
	Then I can buy what is in cart

#Scenario: After I purchase an item my cart is empty
Scenario: Cart empty after purchase
	Given I purchased an item
	And I click on Cart page
	Then My cart is empty

#Given I have 1500$ and I want to buy a phone, laptop and a monitor, create 1 Scenario to buy all in this budget.
@EmptyCart
Scenario: Buy a phone, laptop and monitor within the limit of 1500$
	Given I am logged in
	And I have a budget of 1500$
	When I add in cart a laptop, monitor and phone that don't exceed my budget
	Then I purchase all from cart

	Scenario: Message not sent
	Given I am on the homepage
	And I click on Contact page
	When I don't fill the required data for a new message 
	Then My message is not sent

Scenario: Message sent
	Given I am on the homepage
	And I click on Contact page
	When I fill the required data for a new message 
	Then My message is sent

@EmptyCart
Scenario:Can't buy a Dell laptop model from 2017
	Given I added in my cart a laptop from 2017
	And I click on Cart page
	And I click on Place order button
	When I dont fill the required data to purchase
	Then I can't buy what is in cart